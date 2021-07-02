using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Room
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public int CategoryId { set; get; }

        [ForeignKey("CategoryId")]
        public virtual Category RoomCategory { set; get; }
    }
}