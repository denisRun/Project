using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
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

        public override bool Equals(object obj)
        {
            if (obj is BookingModel)
            {
                var objCM = obj as BookingModel;
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