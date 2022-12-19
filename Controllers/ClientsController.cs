using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eight_Evid_01.Models;
using Eight_Evid_01.ViewMidels;

namespace Eight_Evid_01.Controllers
{
    public class ClientsController : Controller
    {
        private BookingDbContext db = new BookingDbContext();

        public ActionResult Index()
        {
            return View(db.Clients.Include(x=>x.BookingEntries).ToList());
        }

        public ActionResult AddNewSpot(int? id)
        {
            ViewBag.spots = new SelectList(db.Spots.ToList(), "SpotId", "SpotName", id.ToString() ?? "");
            return PartialView("_addSpot");
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create( ClientVM clientVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client()
                {
                    ClientName = clientVM.ClientName,
                    BirthDate = clientVM.BirthDate,
                    Age = clientVM.Age,
                    MaritalState = clientVM.MaritalState
                };
                HttpPostedFileBase file = clientVM.PictureFile;
                if (file!=null)
                {
                    string filePath= Path.Combine("/Images/",Guid.NewGuid().ToString()+Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    client.Picture = filePath;
                }

                foreach (var item in spotId)
                {
                    BookingEntry bookingEntry = new BookingEntry()
                    {
                        Client = client,
                        ClientId = client.ClientId,
                        SpotId = item
                    };
                    db.BookingEntries.Add(bookingEntry);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            Client client = db.Clients.First(x => x.ClientId == id);
            ClientVM clientVM = new ClientVM()
            {
                ClientId= client.ClientId,
                ClientName = client.ClientName,
                BirthDate = client.BirthDate,
                Age = client.Age,
                MaritalState = client.MaritalState
            };

            var clientSpots=db.BookingEntries.Where(x=>x.ClientId== id).ToList();
            foreach (var item in clientSpots)
            {
                clientVM.SpotList.Add(item.SpotId);
            }
            return View(clientVM);
        }


        [HttpPost]
        public ActionResult Edit(ClientVM clientVM, int[] spotId)
        {
            if (ModelState.IsValid)
            {
                Client client = new Client()
                {
                    ClientName = clientVM.ClientName,
                    BirthDate = clientVM.BirthDate,
                    Age = clientVM.Age,
                    MaritalState = clientVM.MaritalState
                };
                HttpPostedFileBase file = clientVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    client.Picture = filePath;
                }

                foreach (var item in spotId)
                {
                    BookingEntry bookingEntry = new BookingEntry()
                    {
                        Client = client,
                        ClientId = client.ClientId,
                        SpotId = item
                    };
                    db.BookingEntries.Add(bookingEntry);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
