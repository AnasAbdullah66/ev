using Eight_Evid_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Eight_Evid_01.ViewMidels
{
    public class ClientVM
    {
        public ClientVM()
        {
            this.SpotList = new List<int>();
        }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Age { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public HttpPostedFileBase PictureFile { get; set; }
        public string Picture { get; set; }
        public bool MaritalState { get; set; }

        public List<int> SpotList { get; set; }

    }
}