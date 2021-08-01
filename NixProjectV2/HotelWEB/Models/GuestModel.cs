using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelWEB.Models
{
    public class GuestModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        [DisplayName("Guest Fullname")]
        public string FullName 
        { 
            get 
            {
                return (Name + " " + Surname);
            } 
        }
    }
}