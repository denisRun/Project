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
        public string Name { set; get; }
        public string Surname { set; get; }
    }
}
