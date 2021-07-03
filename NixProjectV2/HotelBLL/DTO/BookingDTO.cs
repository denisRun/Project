using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class BookingDTO
    {
        public int Id { set; get; }
        public GuestDTO BookingGuest { set; get; }
        public RoomDTO BookingRoom { set; get; }
        public DateTime BookingDate { set; get; }
        public DateTime EnterDate { set; get; }
        public DateTime LeaveDate { set; get; }
        public string Set { set; get; }
    }
}
