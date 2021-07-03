using HotelDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.EF
{
    public class HotelInitializer : DropCreateDatabaseIfModelChanges<HotelContext>
    {
        private void GuestInitializer(HotelContext context)
        {
            var guestList = new List<Guest>()
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
            foreach (var guest in guestList)
            {
                context.Guests.Add(guest);
            }

            context.SaveChanges();
        }

        private void RoomInitializer(HotelContext context)
        {
            var roomList = new List<Room>()
            {
                new Room()
                {
                    Id=1,
                    Name="Room1",
                    CategoryId=1
                },
                new Room()
                {
                    Id=2,
                    Name="Room2",
                    CategoryId=2
                }
            };
            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }

            context.SaveChanges();
        }
        private void CategoryInitializer(HotelContext context)
        {
            var categoryList = new List<Category>()
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
            foreach (var category in categoryList)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();
        }
        private void BookingInitializer(HotelContext context)
        {
            var bookingList = new List<Booking>()
            {
                new Booking()
                {
                    Id=1,
                    GuestId=1,
                    RoomId=1,
                    BookingDate=new DateTime(2021,1,1),
                    EnterDate=new DateTime(2021,1,1),
                    LeaveDate=new DateTime(2021,1,11),
                    Set="yes"
                },
                new Booking()
                {
                    Id=2,
                    GuestId=2,
                    RoomId=2,
                    BookingDate=new DateTime(2021,2,2),
                    EnterDate=new DateTime(2021,2,2),
                    LeaveDate=new DateTime(2021,2,22),
                    Set="no"
                }
            };
            foreach (var booking in bookingList)
            {
                context.Bookings.Add(booking);
            }

            context.SaveChanges();
        }
        protected override void Seed(HotelContext context)
        {
            GuestInitializer(context);
            CategoryInitializer(context);
            RoomInitializer(context);
            BookingInitializer(context);
        }
    }
}
