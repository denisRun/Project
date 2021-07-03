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
        IEnumerable<GuestDTO> GetAllGuests();
        GuestDTO Get(int id);

        void Create(GuestDTO id);
        void Delete(int id);
    }
}
