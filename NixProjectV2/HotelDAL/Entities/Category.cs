using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { set; get; }
        //[Required]
        public string Name { set; get; }
        //[Required]
        public decimal Price { set; get; }
        //[Required]
        public int Bed { set; get; }
        //[Required]
        public DateTime ActionTime { get; set; }
        //[Required]
        public string ActionType { get; set; }
        //[Required]
        public int ActionUserId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                var objCM = obj as Category;
                return this.Id == objCM.Id &&
                    this.Name == objCM.Name &&
                    this.Price == objCM.Price &&
                    this.Bed == objCM.Bed;
            }
            else
            {
                return base.Equals(obj);
            }

        }
    }
}
