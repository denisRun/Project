using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
{
    public class Guest
    {
        [Key]
        public int Id { set; get; }
        //[Required]
        public string Name { set; get; }
        //[Required]
        public string Surname { set; get; }
        //[Required]
        public DateTime ActionTime { get; set; }
        //[Required]
        public string ActionType { get; set; }
        //[Required]
        public int ActionUserId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Guest)
            {
                var objCM = obj as Guest;
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
