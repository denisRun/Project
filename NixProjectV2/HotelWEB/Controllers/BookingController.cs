using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWEB.Controllers
{
    public class BookingController : Controller
    {
        IBookingService service;
        IGuestService serviceGuest;
        IRoomService serviceRoom;
        IMapper mapper;
        IMapper mapperToDTO;
        IMapper mapperGuest;
        IMapper mapperGuestToModel;
        IMapper mapperRoom;
        IMapper mapperRoomToModel;

        public BookingController(IBookingService service, IGuestService serviceGuest,
            IRoomService serviceRoom)
        {
            this.service = service;
            this.serviceGuest = serviceGuest;
            this.serviceRoom = serviceRoom;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();
            this.mapperToDTO = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingModel, BookingDTO>()).CreateMapper();
            this.mapperGuest = new MapperConfiguration(cfg =>
               cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
            this.mapperGuestToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
            this.mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
            this.mapperRoomToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
        }
        // GET: Booking
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(service.GetAllBookings());

            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = mapper.Map<BookingDTO, BookingModel>(service.Get(id));

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var guests = mapperGuest.Map<IEnumerable<GuestDTO>, List<GuestModel>>(serviceGuest.GetAllGuests());
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomModel>>(serviceRoom.GetAllRooms());
            SelectList guestsList = new SelectList(guests, "Id", "FullName");
            SelectList roomsList = new SelectList(rooms, "Id", "Name");
            ViewBag.Guests = guestsList;
            ViewBag.Rooms = roomsList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(BookingModel model)
        {
            model.BookingDate = DateTime.Now;
            model.UserId = 1;
            //model.BookingGuest = mapperGuestToModel.Map<GuestDTO, GuestModel>
            //    (serviceGuest.Get(model.BookingGuest.Id));
            //model.BookingRoom = mapperRoomToModel.Map<RoomDTO, RoomModel>
            //    (serviceRoom.Get(model.BookingRoom.Id));

            if (ModelState.IsValid)
            {
                var modelDTO = mapperToDTO.Map<BookingModel, BookingDTO>(model);
                service.Create(modelDTO);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var guests = mapperGuest.Map<IEnumerable<GuestDTO>, List<GuestModel>>(serviceGuest.GetAllGuests());
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomModel>>(serviceRoom.GetAllRooms());
            SelectList guestsList = new SelectList(guests, "Id", "FullName");
            SelectList roomsList = new SelectList(rooms, "Id", "Name");
            ViewBag.Guests = guestsList;
            ViewBag.Rooms = roomsList;

            var data = mapper.Map<BookingDTO, BookingModel>(service.Get(id));

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(BookingModel model)
        {
            model.UserId = 1;
            if (ModelState.IsValid)
            {
                var modelDTO = mapperToDTO.Map<BookingModel, BookingDTO>(model);
                service.Update(modelDTO.Id, modelDTO);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}