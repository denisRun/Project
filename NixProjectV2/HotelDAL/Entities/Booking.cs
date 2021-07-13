using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
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

        public override bool Equals(object obj)
        {
            if (obj is Booking)
            {
                var objCM = obj as Booking;
                return this.Id == objCM.Id &&
                    this.BookingGuest.Equals(objCM.BookingGuest) &&
                    this.BookingRoom.Equals(objCM.BookingRoom) &&
                    this.BookingDate.Equals(objCM.BookingDate) &&
                    this.EnterDate.Equals(objCM.EnterDate) &&
                    this.LeaveDate.Equals(objCM.LeaveDate) &&
                    this.Set == objCM.Set;
            }
            else
            {
                return base.Equals(obj);
            }

        }
    }
}
