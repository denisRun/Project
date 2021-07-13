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
    public class GuestControllerTest
    {
        private Mock<IGuestService> GuestServiceMock;
        private Mock<IWorkUnit> EFWorkUnitMock;
        private IMapper mapper;
        private HttpRequestMessage request;
        private HttpConfiguration config;

        public GuestControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            GuestServiceMock = new Mock<IGuestService>();
            request = new HttpRequestMessage();
            config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestModel, GuestDTO>()
            ).CreateMapper();
        }

        [TestMethod]
        public void GuestGetByIdTypeIsGuestModel()
        {
            int id = 1;

            GuestServiceMock.Setup(a => a.Get(id)).Returns(new GuestDTO());

            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<GuestModel>();

            Assert.IsInstanceOfType(result.Result, typeof(GuestModel));
        }

        [TestMethod]
        public void GuestGetByIdNegative()
        {
            int id = -1;

            EFWorkUnitMock.Setup(a => a.Guests.Get(id)).Returns(new Guest());
            GuestServiceMock.Setup(a => a.Get(id)).Returns(new GuestDTO());

            var guestService = new GuestService(EFWorkUnitMock.Object);
            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request, id);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GuestGetTypeIsGuestModel()
        {
            GuestServiceMock.Setup(a => a.GetAllGuests()).Returns(new List<GuestDTO>());

            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<GuestModel>>();

            Assert.IsInstanceOfType(result.Result, typeof(List<GuestModel>));
        }

        [TestMethod]
        public void GuestGetByIdIsNotNull()
        {
            int id = 1;

            EFWorkUnitMock.Setup(a => a.Guests.Get(id)).Returns(new Guest());
            GuestServiceMock.Setup(a => a.Get(id)).Returns(new GuestDTO());

            var guestService = new GuestService(EFWorkUnitMock.Object);
            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<GuestModel>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void GuestGetAllIsNotNull()
        {
            EFWorkUnitMock.Setup(a => a.Guests.GetAll()).Returns(new List<Guest>());
            GuestServiceMock.Setup(a => a.GetAllGuests()).Returns(new List<GuestDTO>());

            var guestService = new GuestService(EFWorkUnitMock.Object);
            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<GuestModel>>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void GuestGetByIdTest()
        {
            int id = 1;
            var guest = TestData.GuestList[id - 1];
            var guestDTO = mapper.Map<Guest, GuestDTO>(guest);
            EFWorkUnitMock.Setup(x => x.Guests.Get(id)).Returns(guest);
            GuestServiceMock.Setup(a => a.Get(id)).Returns(guestDTO);

            var guestService = new GuestService(EFWorkUnitMock.Object);
            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<GuestModel>();
            GuestModel expected = mapper.Map<GuestDTO, GuestModel>(guestService.Get(id));

            Assert.AreEqual(expected, result.Result);

        }



        [TestMethod]
        public void GuestGetAllTest()
        {
            EFWorkUnitMock.Setup(a => a.Guests.GetAll()).Returns(new List<Guest>());
            GuestServiceMock.Setup(a => a.GetAllGuests()).Returns(new List<GuestDTO>());

            var guestService = new GuestService(EFWorkUnitMock.Object);
            GuestController controller = new GuestController(GuestServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<GuestModel>>();

            List<GuestModel> expected = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(guestService.GetAllGuests());

            CollectionAssert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void GuestPostTest()
        {
            GuestModel guest = new GuestModel()
            {
                Id = 8,
                Name = "Guest8",
                Surname = "GUest8"
            };

            GuestController controller = new GuestController(GuestServiceMock.Object);
            var response = controller.Post(request, guest);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void GuestPutTest()
        {
            var id = 1;
            var guest = TestData.GuestList[id - 1];

            var guestDTO = mapper.Map<Guest, GuestDTO>(guest);

            GuestServiceMock.Setup(a => a.Get(id)).Returns(guestDTO);
            GuestController controller = new GuestController(GuestServiceMock.Object);
            var response = controller.Put(request, id, mapper.Map<GuestDTO, GuestModel>(guestDTO));
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void GuestDeleteTest()
        {
            var id = 1;
            var guest = TestData.GuestList[id - 1];

            var guestDTO = mapper.Map<Guest, GuestDTO>(guest);

            GuestServiceMock.Setup(a => a.Get(id)).Returns(guestDTO);
            GuestController controller = new GuestController(GuestServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void GuestDeleteTestNegative()
        {
            var id = 1;
            Guest guest = null;

            var guestDTO = mapper.Map<Guest, GuestDTO>(guest);

            GuestServiceMock.Setup(a => a.Get(id)).Returns(guestDTO);
            GuestController controller = new GuestController(GuestServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreNotEqual(HttpStatusCode.OK, result);
        }
    }
}
