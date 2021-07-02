using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public int Bed { set; get; }

    }
}