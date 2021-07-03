using AutoMapper;
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

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Booking, BookingDTO>()
            ).CreateMapper();

            return mapper.Map<IEnumerable<Booking>, List<BookingDTO>>(Database.Bookings.GetAll());
        }

        public BookingDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Booking, BookingDTO>()
            ).CreateMapper();

            return mapper.Map<Booking, BookingDTO>(Database.Bookings.Get(id));
        }

        public void Create(BookingDTO value)
        {
            var data = new Booking()
            {
                BookingGuest = new Guest()
                {
                    Id = value.BookingGuest.Id,
                    Name = value.BookingGuest.Name,
                    Surname = value.BookingGuest.Surname
                },
                BookingRoom = new Room()
                {
                    Id = value.BookingRoom.Id,
                    Name = value.BookingRoom.Name,
                    RoomCategory = new Category()
                    {
                        Id = value.BookingRoom.RoomCategory.Id,
                        Name = value.BookingRoom.RoomCategory.Name,
                        Price = value.BookingRoom.RoomCategory.Price,
                        Bed = value.BookingRoom.RoomCategory.Bed
                    }
                },
                BookingDate = value.BookingDate,
                EnterDate = value.EnterDate,
                LeaveDate = value.LeaveDate,
                Set = value.Set
            };
            Database.Bookings.Create(data);
            Database.Save();
        }

        public void Update(int id, BookingDTO value)
        {
            var data = new Booking()
            {
                BookingGuest = new Guest()
                {
                    Id = value.BookingGuest.Id,
                    Name = value.BookingGuest.Name,
                    Surname = value.BookingGuest.Surname
                },
                BookingRoom = new Room()
                {
                    Id = value.BookingRoom.Id,
                    Name = value.BookingRoom.Name,
                    RoomCategory = new Category()
                    {
                        Id = value.BookingRoom.RoomCategory.Id,
                        Name = value.BookingRoom.RoomCategory.Name,
                        Price = value.BookingRoom.RoomCategory.Price,
                        Bed = value.BookingRoom.RoomCategory.Bed
                    }
                },
                BookingDate = value.BookingDate,
                EnterDate = value.EnterDate,
                LeaveDate = value.LeaveDate,
                Set = value.Set
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
