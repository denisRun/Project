﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
{
    public class Room
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public int CategoryId { set; get; }
        public DateTime ActionTime { get; set; }
        public string ActionType { get; set; }
        public int ActionUserId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category RoomCategory { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is Room)
            {
                var objCM = obj as Room;
                return this.Id == objCM.Id &&
                    this.Name == objCM.Name &&
                    this.RoomCategory.Equals(objCM.RoomCategory);
            }
            else
            {
                return base.Equals(obj);
            }

        }
    }
}
