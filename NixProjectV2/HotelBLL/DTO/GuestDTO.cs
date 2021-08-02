using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class GuestDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public DateTime ActionTime { get; set; }
        public string ActionType { get; set; }
        public int ActionUserId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GuestDTO)
            {
                var objCM = obj as GuestDTO;
                return this.Id == objCM.Id &&
                    this.Name == objCM.Name &&
                    this.Surname.Equals(objCM.Surname);
            }
            else
            {
                return base.Equals(obj);
            }

        }
    }
}
