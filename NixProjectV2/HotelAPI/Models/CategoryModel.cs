using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class CategoryModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is CategoryModel)
            {
                var objCM = obj as CategoryModel;
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