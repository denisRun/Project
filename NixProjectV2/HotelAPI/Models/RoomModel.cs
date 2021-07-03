using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class RoomModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public CategoryModel RoomCategory { set; get; }
    }
}