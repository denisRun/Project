using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class UserModel
    {
        public int Id { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string Login { set; get; }
        [Required]
        public string Password { set; get; }
    }
}