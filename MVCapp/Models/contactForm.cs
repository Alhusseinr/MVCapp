using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCapp.Models
{
    public class contactForm
    {
        [Key]
        public int contactFormId { get; set; }

        public string firstName { get; set; }
        public string email { get; set; }
        public string Subject { get; set; }
        public string msg { get; set; }
    }
}