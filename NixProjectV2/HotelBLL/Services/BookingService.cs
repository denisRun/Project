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
    }
}
