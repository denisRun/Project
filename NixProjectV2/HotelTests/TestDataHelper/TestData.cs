using HotelDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelTests.TestDataHelper
{
    class TestData
    {
        public static List<Category> CategoryList
        {
            get
            {
                return new List<Category>()
                {
                    new Category()
                    {
                        Id=1,
                        Name="Category1",
                        Price=1,
                        Bed=1
                    },
                    new Category()
                    {
                        Id=2,
                        Name="Category2",
                        Price=2,
                        Bed=2
                    }
                };
            }
        }

        public static List<Room> RoomList
        {
            get
            {
                return new List<Room>()
                {
                    new Room()
                    {
                        Id=1,
                        Name="Room1",
                        CategoryId=1,
                        RoomCategory = CategoryList[0]
                    },
                    new Room()
                    {
                        Id=2,
                        Name="Room2",
                        CategoryId=2,
                        RoomCategory = CategoryList[1]
                    }
                };
            }
        }

        public static List<Guest> GuestList
        {
            get
            {
                return new List<Guest>()
                {
                    new Guest()
                    {
                        Id=1,
                        Name="Guest1",
                        Surname="Guest1"
                    },
                    new Guest()
                    {
                        Id=2,
                        Name="Guest2",
                        Surname="Guest2"
                    }
                };
            }
        }

        public static List<Booking> BookingList
        {
            get
            {
                return new List<Booking>()
                {
                    new Booking()
                    {
                        Id=1,
                        GuestId=1,
                        RoomId=1,
                        BookingDate=new DateTime(2021,1,1),
                        EnterDate=new DateTime(2021,1,1),
                        LeaveDate=new DateTime(2021,1,11),
                        Set="yes",
                        BookingGuest = GuestList[0],
                        BookingRoom = RoomList[0]
                    },
                    new Booking()
                    {
                        Id=2,
                        GuestId=2,
                        RoomId=2,
                        BookingDate=new DateTime(2021,2,2),
                        EnterDate=new DateTime(2021,2,2),
                        LeaveDate=new DateTime(2021,2,22),
                        Set="no",
                        BookingGuest = GuestList[0],
                        BookingRoom = RoomList[0]
                    }
                };
            }
        }
    }
}
