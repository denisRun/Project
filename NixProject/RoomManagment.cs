using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NixProject
{
    class RoomManagment
    {
        private List<Room> RoomList;
        private List<Guest> GuestList;
        private List<Order> OrderList;

        public RoomManagment()
        {
            RoomList = new List<Room>();
            GuestList = new List<Guest>();
            OrderList = new List<Order>();

            ReadFromFile();
        }

        public void AddRoom(Room room)
        {
            foreach(var i in RoomList)
            {
                if(i.Number == room.Number)
                    return;
            }
            RoomList.Add(room);
        }

        public void AddGuest(Guest guest)
        {
            foreach (var i in GuestList)
            {
                if (i.FullName == guest.FullName)
                    return;
            }
            GuestList.Add(guest);
        }

        public void DeleteRoom(int roomNumber)
        {
            foreach(var i in RoomList.ToList())
            {
                if(i.Number == roomNumber)
                {
                    RoomList.Remove(i);
                    break;
                }
            }
        }

        public void BookRoom(Room room, Guest guest, DateTime startDate, DateTime endDate)
        {
            if (!GuestList.Contains(guest))
                GuestList.Add(guest);

            if (RoomList.Contains(room)) 
            {
                bool isFree = true;
                foreach (var i in OrderList)
                {
                    if(i.RoomNumber == room.Number &&
                        ((i.Entry<startDate && i.Departure>startDate) ||
                        (i.Entry>=startDate && i.Entry<endDate)))
                    {
                        isFree = false;
                        break;
                    }
                }

                if (isFree)
                {
                    Order booked = new Order(guest.FullName, room.Number, startDate, endDate, true, false, false);
                    OrderList.Add(booked) ;
                }
            }
        }

        public Room GetRoomByNumber(int number)
        {
            Room room;
            var result = from t in RoomList
                     where t.Number == number
                     select t;
            room = result as Room;
            return room;
        }

        public Guest GetGuestByNumber(string fullname)
        {
            Guest guest;
            var result = from t in GuestList
                           where t.FullName == fullname
                           select t;
            guest = result as Guest;
            return guest;
        }

        public int FreePlacesAmount(DateTime startDate, DateTime endDate)
        {
            int busyRoomsCount = 0;

            busyRoomsCount = (from i in OrderList
                      where (i.Entry < startDate && i.Departure > startDate) ||
                            (i.Entry >= startDate && i.Entry < endDate)
                      select i).Count();

            return RoomList.Count() - busyRoomsCount;
        }

        public void CheckIn(Guest guest, Room room, DateTime startDate, DateTime endDate)
        {
            if(GuestList.Contains(guest) && RoomList.Contains(room))
            {
                int index=-1;
                foreach (var i in OrderList)
                {
                    if(guest.FullName == i.GuestFullName && room.Number==i.RoomNumber &&
                        i.Entry == startDate && i.Departure == endDate)
                    {
                        index = OrderList.IndexOf(i);
                        break;
                    }
                }
                if (index != -1)
                    OrderList[index].CheckIn = true;
            }
        }

        public void CheckOut(Guest guest, Room room, DateTime startDate, DateTime endDate)
        {
            if (GuestList.Contains(guest) && RoomList.Contains(room))
            {
                int index = -1;
                foreach (var i in OrderList)
                {
                    if (guest.FullName == i.GuestFullName && room.Number == i.RoomNumber &&
                        i.Entry == startDate && i.Departure == endDate)
                    {
                        index = OrderList.IndexOf(i);
                        break;
                    }
                }
                if (index != -1)
                    OrderList[index].CheckOut = true;
            }
        }

        public void ReadFromFile()
        {
            using (var f = File.Open("Guest.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamReader(f))
                {
                    int count = Convert.ToInt32(stream.ReadLine());

                    for (int i = 0; i < count; i++)
                    {
                        string fullName = stream.ReadLine();
                        DateTime birthDate = Convert.ToDateTime(stream.ReadLine());
                        string residense = stream.ReadLine();
                        Guest guest = new Guest(fullName, birthDate, residense);
                        GuestList.Add(guest);
                    }
                }
            }

            using (var f = File.Open("Room.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamReader(f))
                {
                    int count = Convert.ToInt32(stream.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        int number = Convert.ToInt32(stream.ReadLine());
                        double price = Convert.ToDouble(stream.ReadLine());
                        int capacity = Convert.ToInt32(stream.ReadLine());
                        string category = stream.ReadLine();
                        Room room = new Room(number, price, capacity, category);
                        RoomList.Add(room);
                    }
                }
            }

            using (var f = File.Open("Order.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamReader(f))
                {
                    int count = Convert.ToInt32(stream.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        string guestFullName = stream.ReadLine();
                        int roomNumber = Convert.ToInt32(stream.ReadLine());
                        DateTime entry = Convert.ToDateTime(stream.ReadLine());
                        DateTime departure = Convert.ToDateTime(stream.ReadLine());
                        bool isBooked = Convert.ToBoolean(stream.ReadLine());
                        bool checkIn = Convert.ToBoolean(stream.ReadLine());
                        bool checkOut = Convert.ToBoolean(stream.ReadLine());
                        Order order = new Order(guestFullName, roomNumber, entry, departure, isBooked, checkIn, checkOut);
                        OrderList.Add(order);
                    }
                }
            }
        }

        public void WriteInFile()
        {
            using (var f = File.Open("Guest.txt", FileMode.Truncate))
            { }
            using (var f = File.Open("Room.txt", FileMode.Truncate))
            { }
            using (var f = File.Open("Order.txt", FileMode.Truncate))
            { }

            using (var f = File.Open("Guest.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamWriter(f))
                {

                    stream.WriteLine(GuestList.Count());
                    foreach (var guest in GuestList)
                    {
                        if (guest != null)
                        {
                            stream.WriteLine($"{guest.FullName}");
                            stream.WriteLine($"{guest.BirthDate}");
                            stream.WriteLine($"{guest.Residence}");
                        }
                    }
                }
            }

            using (var f = File.Open("Room.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamWriter(f))
                {
                    stream.WriteLine(RoomList.Count());
                    foreach (var room in RoomList)
                    {
                        if (room != null)
                        {
                            stream.WriteLine($"{room.Number}");
                            stream.WriteLine($"{room.Price}");
                            stream.WriteLine($"{room.Capacity}");
                            stream.WriteLine($"{room.Category}");
                        }
                    }
                }
            }

            using (var f = File.Open("Order.txt", FileMode.OpenOrCreate))
            {
                using (var stream = new StreamWriter(f))
                {
                    stream.WriteLine(OrderList.Count());
                    foreach (var order in OrderList)
                    {
                        if (order != null)
                        {
                            stream.WriteLine($"{order.GuestFullName}");
                            stream.WriteLine($"{order.RoomNumber}");
                            stream.WriteLine($"{order.Entry}");
                            stream.WriteLine($"{order.Departure}");
                            stream.WriteLine($"{order.IsBooked}");
                            stream.WriteLine($"{order.CheckIn}");
                            stream.WriteLine($"{order.CheckOut}");
                        }
                    }
                }
            }
    }
    }
}
