using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Services;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using HotelTests.TestDataHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelTests.ServicesTest
{
    [TestClass]
    public class BookingServiceTest
    {
        Mock<IWorkUnit> EFWorkUnitMock;
        IMapper mapper;
        List<Booking> bookings;

        public BookingServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();
            bookings = TestData.BookingList;
        }

        [TestMethod]
        public void BookingGetAlCategorieslTest()
        {
            EFWorkUnitMock.Setup(a => a.Bookings.GetAll()).Returns(bookings);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            var result = bookingService.GetAllBookings().ToList();
            var expexted = mapper.Map<List<Booking>, List<BookingDTO>>(bookings);

            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void BookingGetTest()
        {
            int id = 1;
            EFWorkUnitMock.Setup(a => a.Bookings.Get(id)).Returns(bookings[id - 1]);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            var result = bookingService.Get(id);
            var expected = mapper.Map<Booking, BookingDTO>(bookings[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BookingCreateTest()
        {
            var booking = new BookingDTO()
            {
                Id = 8,
                Set = "no",
                BookingRoom = mapper.Map<Room, RoomDTO>(TestDataHelper.TestData.RoomList[0]),
                BookingGuest = mapper.Map<Guest, GuestDTO>(TestDataHelper.TestData.GuestList[0]),
                EnterDate = bookings[0].EnterDate,
                LeaveDate = bookings[0].LeaveDate,
                BookingDate = bookings[0].BookingDate
            };
            var data = new Booking()
            {
                Id = 8,
                Set = "no",
                BookingRoom = TestDataHelper.TestData.RoomList[0],
                BookingGuest = TestDataHelper.TestData.GuestList[0],
                EnterDate = bookings[0].EnterDate,
                LeaveDate = bookings[0].LeaveDate,
                BookingDate = bookings[0].BookingDate,
                GuestId = TestDataHelper.TestData.GuestList[0].Id,
                RoomId = TestDataHelper.TestData.RoomList[0].Id
            };
            EFWorkUnitMock.Setup(a => a.Bookings.Create(data));
            var bookingService = new BookingService(EFWorkUnitMock.Object);

            bookingService.Create(booking);
            EFWorkUnitMock.Verify(a => a.Bookings.Create(data));
        }

        [TestMethod]
        public void BookingUpdateTest()
        {
            var id = 2;
            var booking = bookings[id - 1];

            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            EFWorkUnitMock.Setup(a => a.Bookings.Update(id, booking));
            var bookingService = new BookingService(EFWorkUnitMock.Object);

            bookingService.Update(id, bookingDTO);
            EFWorkUnitMock.Verify(a => a.Bookings.Update(id, booking));
        }

        [TestMethod]
        public void BookingDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(a => a.Bookings.Delete(id));
            var bookingService = new BookingService(EFWorkUnitMock.Object);

            bookingService.Delete(id);
            EFWorkUnitMock.Verify(a => a.Bookings.Delete(id));
        }
    }
}
