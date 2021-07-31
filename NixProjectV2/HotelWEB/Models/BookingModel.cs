using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class BookingModel
    {
        public int Id { set; get; }
        public int UserId { set; get; }
        public GuestModel BookingGuest { set; get; }
        public RoomModel BookingRoom { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime LeaveDate { set; get; }
        public string Set { set; get; }
    }
}