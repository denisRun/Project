using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Booking
    {
        [Key]
        public int Id { set; get; }
        public int GuestId { set; get; }
        public int RoomId { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime LeaveDate { set; get; }
        public string Set { set; get; }

        [ForeignKey("GuestId")]
        public virtual Guest BookingGuest { set; get; }
        [ForeignKey("RoomId")]
        public virtual Room BookingRoom { set; get; }
    }
}