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
    public class RoomServiceTest
    {
        Mock<IWorkUnit> EFWorkUnitMock;
        IMapper mapper;
        List<Room> rooms;

        public RoomServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            rooms = TestData.RoomList;
        }

        [TestMethod]
        public void RoomGetAlCategorieslTest()
        {
            EFWorkUnitMock.Setup(a => a.Rooms.GetAll()).Returns(rooms);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            var result = roomService.GetAllRooms().ToList();
            var expexted = mapper.Map<List<Room>, List<RoomDTO>>(rooms);

            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void RoomGetTest()
        {
            int id = 1;
            EFWorkUnitMock.Setup(a => a.Rooms.Get(id)).Returns(rooms[id - 1]);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            var result = roomService.Get(id);
            var expected = mapper.Map<Room, RoomDTO>(rooms[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoomCreateTest()
        {
            var room = new RoomDTO()
            {
                Id = 8,
                Name = "Room8",
                RoomCategory = mapper.Map<Category, CategoryDTO>(TestDataHelper.TestData.CategoryList[0])
            };
            var data = new Room()
            {
                Id = 8,
                Name = "Room8",
                CategoryId = TestDataHelper.TestData.CategoryList[0].Id,
                RoomCategory = TestDataHelper.TestData.CategoryList[0]
            };
            EFWorkUnitMock.Setup(a => a.Rooms.Create(data));
            var roomService = new RoomService(EFWorkUnitMock.Object);

            roomService.Create(room);
            EFWorkUnitMock.Verify(a => a.Rooms.Create(data));
        }

        [TestMethod]
        public void RoomUpdateTest()
        {
            var id = 2;
            var room = rooms[id - 1];

            var roomDTO = mapper.Map<Room, RoomDTO>(room);

            EFWorkUnitMock.Setup(a => a.Rooms.Update(id, room));
            var roomService = new RoomService(EFWorkUnitMock.Object);

            roomService.Update(id, roomDTO);
            EFWorkUnitMock.Verify(a => a.Rooms.Update(id, room));
        }

        [TestMethod]
        public void RoomDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(a => a.Rooms.Delete(id));
            var roomService = new RoomService(EFWorkUnitMock.Object);

            roomService.Delete(id);
            EFWorkUnitMock.Verify(a => a.Rooms.Delete(id));
        }
    }
}
