using AutoMapper;
using HotelAPI.Controllers;
using HotelAPI.Models;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelBLL.Services;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using HotelTests.TestDataHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace HotelTests.ControllersTest
{
    [TestClass]
    public class BookingControllerTest
    {
        private Mock<IBookingService> BookingServiceMock;
        private Mock<IWorkUnit> EFWorkUnitMock;
        private IMapper mapper;
        private HttpRequestMessage request;
        private HttpConfiguration config;

        public BookingControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            BookingServiceMock = new Mock<IBookingService>();
            request = new HttpRequestMessage();
            config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingModel, BookingDTO>()
            ).CreateMapper();
        }

        [TestMethod]
        public void BookingGetByIdTypeIsBookingModel()
        {
            int id = 1;
            var booking = TestData.BookingList[id - 1];
            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);

            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<BookingModel>();

            Assert.IsInstanceOfType(result.Result, typeof(BookingModel));
        }

        [TestMethod]
        public void BookingGetByIdNegative()
        {
            int id = -1;

            EFWorkUnitMock.Setup(a => a.Bookings.Get(id)).Returns(new Booking());
            BookingServiceMock.Setup(a => a.Get(id)).Returns(new BookingDTO());

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request, id);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void BookingGetTypeIsBookingModel()
        {
            BookingServiceMock.Setup(a => a.GetAllBookings()).Returns(new List<BookingDTO>());

            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<BookingModel>>();

            Assert.IsInstanceOfType(result.Result, typeof(List<BookingModel>));
        }

        [TestMethod]
        public void BookingGetByIdIsNotNull()
        {
            int id = 1;
            var booking = TestData.BookingList[id - 1];
            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            EFWorkUnitMock.Setup(a => a.Bookings.Get(id)).Returns(booking);
            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<BookingModel>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void BookingGetAllIsNotNull()
        {
            EFWorkUnitMock.Setup(a => a.Bookings.GetAll()).Returns(new List<Booking>());
            BookingServiceMock.Setup(a => a.GetAllBookings()).Returns(new List<BookingDTO>());

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<BookingModel>>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void BookingGetByIdTest()
        {
            int id = 1;
            var booking = TestData.BookingList[id - 1];
            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);
            EFWorkUnitMock.Setup(a => a.Bookings.Get(id)).Returns(booking);
            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<BookingModel>();

            BookingModel expected = mapper.Map<BookingDTO, BookingModel>(bookingService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }



        [TestMethod]
        public void BookingGetAllTest()
        {
            EFWorkUnitMock.Setup(a => a.Bookings.GetAll()).Returns(new List<Booking>());
            BookingServiceMock.Setup(a => a.GetAllBookings()).Returns(new List<BookingDTO>());

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<BookingModel>>();

            List<BookingModel> expected = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(bookingService.GetAllBookings());

            CollectionAssert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void BookingMoneyPerMonthGetTest ()
        {
            var bookings = TestData.BookingList;
            var bookingsDTO = mapper.Map<List<Booking>, List<BookingDTO>>(bookings);
            EFWorkUnitMock.Setup(a => a.Bookings.GetAll()).Returns(bookings);
            BookingServiceMock.Setup(a => a.GetAllBookings()).Returns(bookingsDTO);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.MoneyPerMonthGet(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void BookingRegistration()
        {
            var id = 1;
            var booking = TestData.BookingList[id - 1];
            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);
            EFWorkUnitMock.Setup(a => a.Bookings.Get(id)).Returns(booking);
            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);

            var bookingService = new BookingService(EFWorkUnitMock.Object);
            BookingController controller = new BookingController(BookingServiceMock.Object);

            var response = controller.RegistrationPut(request, id);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void BookingPostTest()
        {
            BookingModel booking = new BookingModel()
            {
                Id = 8,
                Set = "yes",
                BookingGuest = mapper.Map<Guest, GuestModel>(TestData.BookingList[0].BookingGuest),
                BookingRoom = mapper.Map<Room, RoomModel>(TestData.BookingList[0].BookingRoom),
                EnterDate = TestData.BookingList[0].EnterDate,
                LeaveDate = TestData.BookingList[0].LeaveDate,
                BookingDate = TestData.BookingList[0].BookingDate
            };

            BookingController controller = new BookingController(BookingServiceMock.Object);
            var response = controller.Post(request, booking);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void BookingPutTest()
        {
            var id = 1;
            var booking = TestData.BookingList[id - 1];

            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);
            BookingController controller = new BookingController(BookingServiceMock.Object);
            var response = controller.Put(request, id, mapper.Map<BookingDTO, BookingModel>(bookingDTO));
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void BookingDeleteTest()
        {
            var id = 1;
            var booking = TestData.BookingList[id - 1];

            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);
            BookingController controller = new BookingController(BookingServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void BookingDeleteTestNegative()
        {
            var id = 1;
            Booking booking = null;

            var bookingDTO = mapper.Map<Booking, BookingDTO>(booking);

            BookingServiceMock.Setup(a => a.Get(id)).Returns(bookingDTO);
            BookingController controller = new BookingController(BookingServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreNotEqual(HttpStatusCode.OK, result);
        }
    }
}
