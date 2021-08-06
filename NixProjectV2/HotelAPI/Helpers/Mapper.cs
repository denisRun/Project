using HotelAPI.Models;
using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Helpers
{
    public class Mapper
    {
        public static BookingDTO MapToBookingDTO(BookingModel value)
        {
            var result = new BookingDTO()
            {
                BookingGuest = new GuestDTO()
                {
                    Id = value.BookingGuest.Id
                },
                BookingRoom = new RoomDTO()
                {
                    Id = value.BookingRoom.Id
                },
                UserId=value.UserId,
                Id = value.Id,
                BookingDate = value.BookingDate,
                EnterDate = value.EnterDate,
                LeaveDate = value.LeaveDate,
                Set = value.Set
            };

            return result;
        }

        public static RoomDTO MapToRoomDTO(RoomModel value)
        {
            var result = new RoomDTO()
            {
                Id = value.Id,
                Name = value.Name,
                RoomCategory = new CategoryDTO()
                {
                    Id = value.RoomCategory.Id
                }
            };

            return result;
        }
    }
}