using System;
using System.Data.Entity;
using System.Linq;

namespace WebApplication1.Models
{
    public class HotelModel : DbContext
    {

        public HotelModel()
            : base("Server=DESKTOP-PC1CLF8;Database=hotel;User Id=nix;Password=nix;MultipleActiveResultSets=True;")
        {
            Database.SetInitializer<HotelModel>(new DropCreateDatabaseAlways<HotelModel>());
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}