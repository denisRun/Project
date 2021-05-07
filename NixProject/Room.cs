using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NixProject
{
    class Room
    {
        public int Number;
        public double Price;
        public int Capacity;
        public string Category;

        public Room(int number, double price, int capacity, string category)
        {
            this.Number = number;
            this.Price = price;
            this.Capacity = capacity;
            this.Category = category;
        }
    }
}
