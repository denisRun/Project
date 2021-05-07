using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NixProject
{
    class Order
    {
        public string GuestFullName;
        public int RoomNumber;
        public DateTime Entry;
        public DateTime Departure;
        public bool IsBooked;
        public bool CheckIn;
        public bool CheckOut;

        public Order(string fullname, int number, DateTime entry, DateTime departure, bool isBooked, bool checkIn, bool checkOut)
        {
            this.GuestFullName = fullname;
            this.RoomNumber = number;
            this.Entry = entry;
            this.Departure = departure;
            this.IsBooked = isBooked;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
        }
    }
}
