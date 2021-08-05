using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelWEB.Models
{
    public class RoomModel
    {
        public int Id { set; get; }
        [DisplayName("Room Name")]
        public string Name { set; get; }
        public int ActionUserId { get; set; }
        public CategoryModel RoomCategory { set; get; }
    }
}