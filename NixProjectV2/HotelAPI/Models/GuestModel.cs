using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class GuestModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }

        public override bool Equals(object obj)
        {
            if (obj is GuestModel)
            {
                var objCM = obj as GuestModel;
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