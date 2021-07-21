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
    public class RoomControllerTest
    {
        private Mock<IRoomService> RoomServiceMock;
        private Mock<IWorkUnit> EFWorkUnitMock;
        private IMapper mapper;
        private HttpRequestMessage request;
        private HttpConfiguration config;

        public RoomControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            RoomServiceMock = new Mock<IRoomService>();
            request = new HttpRequestMessage();
            config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomModel, RoomDTO>()
            ).CreateMapper();
        }

        [TestMethod]
        public void RoomGetByIdTypeIsRoomModel()
        {
            int id = 1;
            var room = TestData.RoomList[id - 1];
            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<RoomModel>();

            Assert.IsInstanceOfType(result.Result, typeof(RoomModel));
        }

        [TestMethod]
        public void RoomGetByIdNegative()
        {
            int id = -1;

            EFWorkUnitMock.Setup(a => a.Rooms.Get(id)).Returns(new Room());
            RoomServiceMock.Setup(a => a.Get(id)).Returns(new RoomDTO());

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request, id);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void RoomGetTypeIsRoomModel()
        {
            RoomServiceMock.Setup(a => a.GetAllRooms()).Returns(new List<RoomDTO>());

            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<RoomModel>>();

            Assert.IsInstanceOfType(result.Result, typeof(List<RoomModel>));
        }

        [TestMethod]
        public void RoomGetByIdIsNotNull()
        {
            int id = 1;
            var room = TestData.RoomList[id - 1];
            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            EFWorkUnitMock.Setup(a => a.Rooms.Get(id)).Returns(room);
            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<RoomModel>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void RoomGetAllIsNotNull()
        {
            EFWorkUnitMock.Setup(a => a.Rooms.GetAll()).Returns(new List<Room>());
            RoomServiceMock.Setup(a => a.GetAllRooms()).Returns(new List<RoomDTO>());

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<RoomModel>>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void RoomGetByIdTest()
        {
            int id = 1;
            var rooms = TestData.RoomList;
            EFWorkUnitMock.Setup(a => a.Rooms.Get(id)).Returns(rooms[id-1]);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            var result = roomService.Get(id);
            var expected = mapper.Map<Room, RoomDTO>(rooms[id - 1]);

            Assert.AreEqual(expected, result);
        }



        [TestMethod]
        public void RoomGetAllTest()
        {
            EFWorkUnitMock.Setup(a => a.Rooms.GetAll()).Returns(new List<Room>());
            RoomServiceMock.Setup(a => a.GetAllRooms()).Returns(new List<RoomDTO>());

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<RoomModel>>();

            List<RoomModel> expected = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomService.GetAllRooms());

            CollectionAssert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void RoomPostTest()
        {
            RoomModel room = new RoomModel()
            {
                Id = 8,
                Name = "Room8",
                RoomCategory = mapper.Map<Category, CategoryModel>(TestData.RoomList[0].RoomCategory)
            };

            RoomController controller = new RoomController(RoomServiceMock.Object);
            var response = controller.Post(request, room);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomPutTest()
        {
            var id = 1;
            var room = TestData.RoomList[id - 1];

            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);
            RoomController controller = new RoomController(RoomServiceMock.Object);
            var response = controller.Put(request, id, mapper.Map<RoomDTO, RoomModel>(roomDTO));
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomDeleteTest()
        {
            var id = 1;
            var room = TestData.RoomList[id - 1];

            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);
            RoomController controller = new RoomController(RoomServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomDeleteTestNegative()
        {
            var id = 1;
            Room room = null;

            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);
            RoomController controller = new RoomController(RoomServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreNotEqual(HttpStatusCode.OK, result);
        }
    }
}
