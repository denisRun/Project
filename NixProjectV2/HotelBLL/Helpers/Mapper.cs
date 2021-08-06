using HotelBLL.DTO;
using HotelDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Helpers
{
    class Mapper
    {
        public static Booking MapToBooking(BookingDTO value)
        {
            var result = new Booking(){
                Id = value.Id,
                UserId = value.UserId,
                GuestId = value.BookingGuest.Id,
                RoomId = value.BookingRoom.Id,
                BookingDate = value.BookingDate,
                EnterDate = value.EnterDate,
                LeaveDate = value.LeaveDate,
                Set = value.Set,
                ActionTime = value.ActionTime,
                ActionType = value.ActionType,
                ActionUserId = value.ActionUserId
            };

            return result;
        }

        public static Room MapToRoom(RoomDTO value)
        {
            var result = new Room()
            {
                Name = value.Name,
                CategoryId = value.RoomCategory.Id,
                ActionType = value.ActionType,
                ActionTime = value.ActionTime,
                ActionUserId = value.ActionUserId
            };

            return result;
        }
    }
}
