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
    class GuestRepository : IRepository<Guest>
    {
        private HotelContext db;

        public GuestRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Guest> GetAll()
        {
            return db.Guests;
        }

        public Guest Get(int id)
        {
            return db.Guests.Find(id);
        }

        public void Create(Guest guest)
        {
            db.Guests.Add(guest);
        }

        public void Update(int guestId, Guest value)
        {
            var guest = db.Guests.FirstOrDefault(m => m.Id == guestId);
            if (guest != null)
            {
                guest.Name = value.Name ?? guest.Name;
                guest.Surname = value.Surname ?? guest.Surname;
            }

        }

        public void Delete(int id)
        {
            Guest guest = Get(id);
            if (guest != null)
                db.Guests.Remove(guest);
        }
    }
}
