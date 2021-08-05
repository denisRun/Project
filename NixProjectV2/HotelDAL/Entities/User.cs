using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
{
    public class User
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Login { set; get; }
        [Required]
        public string Password { set; get; }
        [Required]
        public string FullName { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                var objCM = obj as User;
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
