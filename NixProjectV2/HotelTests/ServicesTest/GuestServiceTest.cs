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
    public class GuestServiceTest
    {
        Mock<IWorkUnit> EFWorkUnitMock;
        IMapper mapper;
        List<Guest> guests;

        public GuestServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
            guests = TestData.GuestList;
        }

        [TestMethod]
        public void GuestGetAlCategorieslTest()
        {
            EFWorkUnitMock.Setup(a => a.Guests.GetAll()).Returns(guests);

            var guestService = new GuestService(EFWorkUnitMock.Object);
            var result = guestService.GetAllGuests().ToList();
            var expexted = mapper.Map<List<Guest>, List<GuestDTO>>(guests);

            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void GuestGetTest()
        {
            int id = 1;
            EFWorkUnitMock.Setup(a => a.Guests.Get(id)).Returns(guests[id - 1]);

            var guestService = new GuestService(EFWorkUnitMock.Object);
            var result = guestService.Get(id);
            var expected = mapper.Map<Guest, GuestDTO>(guests[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GuestCreateTest()
        {
            var guest = new GuestDTO()
            {
                Id = 8,
                Name = "Guest8",
                Surname = "Guest8"
            };

            EFWorkUnitMock.Setup(a => a.Guests.Create(mapper.Map<GuestDTO, Guest>(guest)));
            var guestService = new GuestService(EFWorkUnitMock.Object);

            guestService.Create(guest);
            EFWorkUnitMock.Verify(a => a.Guests.Create(mapper.Map<GuestDTO, Guest>(guest)));
        }

        [TestMethod]
        public void GuestUpdateTest()
        {
            var id = 1;
            var guest = guests[id - 1];
            var guestDTO = mapper.Map<Guest, GuestDTO>(guest);

            EFWorkUnitMock.Setup(a => a.Guests.Update(id, guest));
            var guestService = new GuestService(EFWorkUnitMock.Object);

            guestService.Update(id, guestDTO);
            EFWorkUnitMock.Verify(a => a.Guests.Update(id, guest));
        }

        [TestMethod]
        public void GuestDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(a => a.Guests.Delete(id));
            var guestService = new GuestService(EFWorkUnitMock.Object);

            guestService.Delete(id);
            EFWorkUnitMock.Verify(a => a.Guests.Delete(id));
        }
    }
}
