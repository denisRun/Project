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
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO Get(int id);
        void Create(CategoryDTO id);
        void Update(int id, CategoryDTO room);
        void Delete(int id);
    }
}
