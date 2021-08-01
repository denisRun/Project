﻿using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Services
{
    public class BookingService : IBookingService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperModelToDto;

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
            mapperModelToDto = new MapperConfiguration(cfg =>
                cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            return mapperModelToDto.Map<IEnumerable<Booking>, List<BookingDTO>>(Database.Bookings.GetAll());
        }

        public BookingDTO Get(int id)
        {
            return mapperModelToDto.Map<Booking, BookingDTO>(Database.Bookings.Get(id));
        }

        public void Create(BookingDTO booking)
        {

            var data = new Booking()
            {
                Id = booking.Id,
                UserId = booking.UserId,
                GuestId = booking.BookingGuest.Id,
                RoomId = booking.BookingRoom.Id,
                BookingDate = booking.BookingDate,
                EnterDate = booking.EnterDate,
                LeaveDate = booking.LeaveDate,
                Set = booking.Set
            };

            Database.Bookings.Create(data);
            Database.Save();
        }

        public void Update(int id, BookingDTO booking)
        {
            var user = Database.Users.Get(booking.UserId);
            var guest = Database.Guests.Get(booking.BookingGuest.Id);
            var room = Database.Rooms.Get(booking.BookingRoom.Id);
            booking.BookingDate = DateTime.Now;

            var data = new Booking()
            {
                Id = booking.Id,
                UserId = booking.UserId,
                GuestId = booking.BookingGuest.Id,
                RoomId = booking.BookingRoom.Id,
                BookingDate = booking.BookingDate,
                EnterDate = booking.EnterDate,
                LeaveDate = booking.LeaveDate,
                Set = booking.Set
            };

            Database.Bookings.Update(id, data);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Bookings.Delete(id);
            Database.Save();
        }
    }
}
