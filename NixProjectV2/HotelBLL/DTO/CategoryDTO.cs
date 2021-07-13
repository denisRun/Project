using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.DTO
{
    public class CategoryDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is CategoryDTO)
            {
                var objCM = obj as CategoryDTO;
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
