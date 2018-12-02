using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCapp.Models
{
    public class Login
    {
        [Key]
        public int LoginKey { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool frgpassword { get; set; }
    }
}