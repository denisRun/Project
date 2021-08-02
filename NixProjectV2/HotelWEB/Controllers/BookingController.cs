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
        IMapper mapperRoom;


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
            this.mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();

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
            if (ModelState.IsValid)
            {
                model.UserId = Convert.ToInt32(User.Identity.Name);
                model.ActionUserId = model.UserId;
                var modelDTO = new BookingDTO()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    BookingDate = model.BookingDate,
                    EnterDate = model.EnterDate,
                    LeaveDate = model.LeaveDate,
                    Set = model.Set,
                    ActionUserId = model.ActionUserId,
                    BookingRoom = new RoomDTO()
                    {
                        Id = model.BookingRoom.Id
                    },
                    BookingGuest = new GuestDTO()
                    {
                        Id = model.BookingGuest.Id
                    }
                };

                service.Create(modelDTO);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Model is invalid");
            return View();
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
            if (ModelState.IsValid)
            {
                model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                var modelDTO = new BookingDTO()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    BookingDate = model.BookingDate,
                    EnterDate = model.EnterDate,
                    LeaveDate = model.LeaveDate,
                    Set = model.Set,
                    ActionUserId = model.ActionUserId,
                    BookingRoom = new RoomDTO()
                    {
                        Id = model.BookingRoom.Id
                    },
                    BookingGuest = new GuestDTO()
                    {
                        Id = model.BookingGuest.Id
                    }
                };

                service.Update(modelDTO.Id, modelDTO);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Model is invalid");
            return View();
        }

        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult CheckIn(int id)
        {
            service.CheckIn(id);
            return RedirectToAction("Index");
        }

        public ActionResult CheckOut(int id)
        {
            service.CheckOut(id);
            return RedirectToAction("Index");
        }

        public ActionResult MoneyPerMonth()
        {
            var bookings = service.GetAllBookings();
            Dictionary<string, decimal> money = new Dictionary<string, decimal>();

            foreach (var booking in bookings)
            {
                string key = booking.EnterDate.ToString("yyyy.MM");
                decimal sum = Decimal.Parse(
                    (booking.LeaveDate - booking.EnterDate).TotalDays.ToString()) *
                    booking.BookingRoom.RoomCategory.Price;

                if (money.ContainsKey(key))
                {
                    money[key] += sum;
                }
                else
                {
                    money.Add(key, sum);
                }               
            }

            return View(money);
        }

    }
}