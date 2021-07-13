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

        public override bool Equals(object obj)
        {
            if (obj is RoomModel)
            {
                var objCM = obj as RoomModel;
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