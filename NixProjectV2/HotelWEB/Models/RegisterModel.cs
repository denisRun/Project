using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class RegisterModel
    {
        [Required]
        public string Login { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string RepeatPassword { set; get; }
        [Required]
        public string FullName{ set; get; }
    }
}