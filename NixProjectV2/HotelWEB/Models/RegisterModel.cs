using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class RegisterModel
    {
        public string Login { set; get; }
        public string Password { set; get; }
        public string RepeatPassword { set; get; }
        public string FullName{ set; get; }
    }
}