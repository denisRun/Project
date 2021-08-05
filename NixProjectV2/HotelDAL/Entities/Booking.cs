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
        //[Required]
        public int UserId { set; get; }
        //[Required]
        public int GuestId { set; get; }
        //[Required]
        public int RoomId { set; get; }
        //[Required]
        public DateTime BookingDate { set; get; }
        //[Required]
        public DateTime EnterDate { set; get; }
        //[Required]
        public DateTime LeaveDate { set; get; }
        public string Set { set; get; }
        //[Required]
        public DateTime ActionTime { get; set; }
        //[Required]
        public string ActionType { get; set; }
        //[Required]
        public int ActionUserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User BookingUser { set; get; }
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
                    this.BookingUser.Equals(objCM.BookingUser) &&
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
