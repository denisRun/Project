using HotelBLL.DTO;
using HotelWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Helpers
{
    public class Mapper
    {
        public static BookingDTO MapToBookingDTO(BookingModel value)
        {
            var result = new BookingDTO()
            {
                Id = value.Id,
                UserId = value.UserId,
                BookingDate = value.BookingDate,
                EnterDate = value.EnterDate,
                LeaveDate = value.LeaveDate,
                Set = value.Set,
                ActionUserId = value.ActionUserId,
                BookingRoom = new RoomDTO()
                {
                    Id = value.BookingRoom.Id
                },
                BookingGuest = new GuestDTO()
                {
                    Id = value.BookingGuest.Id
                }
            };

            return result;
        }

        public static RoomDTO MapToRoomDTO(RoomModel value)
        {
            var result = new RoomDTO()
            {
                Id = value.Id,
                Name = value.Name,
                ActionUserId = value.ActionUserId,
                RoomCategory = new CategoryDTO()
                {
                    Id = value.RoomCategory.Id
                }
            };

            return result;
        }
    }
}