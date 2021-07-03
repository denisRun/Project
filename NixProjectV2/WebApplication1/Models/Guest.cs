using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Guest
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
    }
}