using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }
    }
}
