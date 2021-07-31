using HotelDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.EF
{
    public class HotelContext : DbContext
    {

        public HotelContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<HotelContext>(new HotelInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
