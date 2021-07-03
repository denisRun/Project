using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetAllRooms();
        BookingDTO Get(int id);
    }
}
