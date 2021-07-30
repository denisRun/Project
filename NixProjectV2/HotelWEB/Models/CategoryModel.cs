using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class CategoryModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }
    }
}