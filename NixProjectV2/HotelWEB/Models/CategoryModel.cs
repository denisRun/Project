using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class CategoryModel
    {
        public int Id { set; get; }
        [DisplayName("Category Name")]
        public string Name { set; get; }
        [DisplayName("Category Price")]
        public decimal Price { set; get; }
        [DisplayName("Lying places")]
        public int Bed { set; get; }
        public int ActionUserId { get; set; }
    }
}