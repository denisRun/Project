using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class RoomDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public CategoryDTO RoomCategory { set; get; }
        public DateTime ActionTime { get; set; }
        public string ActionType { get; set; }
        public int ActionUserId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is RoomDTO)
            {
                var objCM = obj as RoomDTO;
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
