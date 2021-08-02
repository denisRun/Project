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
    public class EFWorkUnit : IWorkUnit
    {
        private HotelContext db;
        private UserRepository userRepository;
        private GuestRepository guestRepository;
        private RoomRepository roomRepository;
        private CategoryRepository categoryRepository;
        private BookingRepository bookingRepository;

        public EFWorkUnit(string connectionString)
        {
            db = new HotelContext(connectionString);
        }

        public IRepository<Guest> Guests
        {
            get
            {
                if (guestRepository == null)
                {
                    guestRepository = new GuestRepository(db);
                }

                return guestRepository;
            }
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }

                return roomRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(db);
                }

                return bookingRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }

                return categoryRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }

                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
