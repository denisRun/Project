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

        public void Delete(int id)
        {
            Booking booking = Get(id);
            if (booking != null)
                db.Bookings.Remove(booking);
        }
    }
}
