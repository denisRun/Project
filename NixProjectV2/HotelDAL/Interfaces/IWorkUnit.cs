using HotelDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Interfaces
{
    public interface IWorkUnit
    {
        IRepository<Room> Rooms { get; }
        IRepository<Category> Categories { get; }
        IRepository<Guest> Guests { get; }
        IRepository<Booking> Bookings { get; }
        void Save();
    }
}
