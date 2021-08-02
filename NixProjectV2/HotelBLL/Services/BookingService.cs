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
            booking.BookingDate = DateTime.Now;
            booking.ActionType = "Create";
            booking.ActionTime = DateTime.Now;
            var data = Helpers.Mapper.MapToBooking(booking);

            Database.Bookings.Create(data);
            Database.Save();
        }

        public void Update(int id, BookingDTO booking)
        {
            booking.BookingDate = Database.Bookings.Get(id).BookingDate;
            booking.ActionType = "Update";
            booking.ActionTime = DateTime.Now;
            var data = Helpers.Mapper.MapToBooking(booking);

            Database.Bookings.Update(id, data);
            Database.Save();
        }

        public void CheckIn(int id)
        {
            string checkStatus = "Checked In";
            var data = Database.Bookings.Get(id);
            data.Set = checkStatus;

            Database.Bookings.Update(id, data);
            Database.Save();
        }

        public void CheckOut(int id)
        {
            string checkStatus = "Checked Out";
            var data = Database.Bookings.Get(id);
            data.Set = checkStatus;

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
