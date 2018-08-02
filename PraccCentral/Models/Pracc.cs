using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraccCentral.Models
{
    public class Pracc
    {
        public int PraccId { get; set; }
        public string Map { get; set; }
        // Time 

        public virtual ApplicationUser User { get; set; }

        //public virtual List<ApplicationUser> Requests { get; set; }
        //public List<ApplicationUser> UserRequests { get; set; }
    }
}