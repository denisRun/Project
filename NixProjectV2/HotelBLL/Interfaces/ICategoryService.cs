using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllRooms();
        CategoryDTO Get(int id);
    }
}
