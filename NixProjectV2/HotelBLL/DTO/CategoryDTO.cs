using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class CategoryDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }
    }
}
