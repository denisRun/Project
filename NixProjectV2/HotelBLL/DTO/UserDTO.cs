using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class UserDTO
    {
        public int Id { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string FullName { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is UserDTO)
            {
                var objCM = obj as UserDTO;
                return this.Id == objCM.Id &&
                    this.Login == objCM.Login &&
                    this.Password == objCM.Password &&
                    this.FullName == objCM.FullName;
            }
            else
            {
                return base.Equals(obj);
            }

        }
    }
}
