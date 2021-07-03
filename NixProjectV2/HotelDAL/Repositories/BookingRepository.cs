using HotelDAL.EF;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Repositories
{
    class BookingRepository : IRepository<Booking>
    {
        private HotelContext db;

        public BookingRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
        }

        public void Update(int BookingId, Booking value)
        {
            var booking = db.Bookings.FirstOrDefault(m => m.Id == BookingId);
            if (booking != null)
            {
                booking.GuestId = value.GuestId != 0 ? value.GuestId : booking.GuestId;
                booking.RoomId = value.RoomId != 0 ? value.RoomId : booking.RoomId;
                booking.Set = value.Set ?? booking.Set;
                booking.BookingDate = value.BookingDate != null ? value.BookingDate : booking.BookingDate;
                booking.EnterDate = value.EnterDate != null ? value.EnterDate : booking.EnterDate;
                booking.LeaveDate = value.LeaveDate != null ? value.LeaveDate : booking.LeaveDate;
            }

        }

        public void Delete(int id)
        {
            Booking booking = Get(id);
            if (booking != null)
                db.Bookings.Remove(booking);
        }
    }
}
