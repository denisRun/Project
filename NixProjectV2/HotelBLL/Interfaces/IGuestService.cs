using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GuestDTO> GetAllRooms();
        GuestDTO Get(int id);
    }
}
