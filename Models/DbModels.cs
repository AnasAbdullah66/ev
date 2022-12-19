using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Eight_Evid_01.Models
{
    public class Client
    {
        public Client()
        {
            this.BookingEntries = new List<BookingEntry>();
        }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Age { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}"),DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; }
        public bool MaritalState { get; set; }

        public ICollection<BookingEntry> BookingEntries { get; set; }

    }

    public class Spot
    {
        public Spot()
        {
            this.BookingEntries=new List<BookingEntry>();
        }
        public int SpotId { get; set; }
        public string SpotName { get; set; }

        public ICollection<BookingEntry> BookingEntries { get; set; }
    }

    public class BookingEntry
    {
        public int BookingEntryId { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        [ForeignKey("Spot")]
        public int SpotId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Spot Spot { get; set; }
    }

    public class BookingDbContext:DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<BookingEntry> BookingEntries { get; set; }
    }

}