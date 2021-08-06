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
            this.mapperGuest = new MapperConfiguration(cfg =>
               cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
            this.mapperRoom = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();

        }

        [Authorize]
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(
                service.GetAllBookings());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = mapper.Map<BookingDTO, BookingModel>(
                service.Get(id));
            return View(data);
        }

        [Authorize]
        public ActionResult DateSettings()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DateSettings(BookingModel booking)
        {
            if (booking.LeaveDate < booking.EnterDate)
            {
                ModelState.AddModelError("LeaveDate",
                    "Leaving date must be greater");
            }
            if ((booking.LeaveDate - booking.EnterDate).Days < 1)
            {
                ModelState.AddModelError("LeaveDate",
                    "You can stay not less than 1 day");
            }

            if (ModelState.IsValidField("EnterDate") &&
                ModelState.IsValidField("LeaveDate"))
            {
                return RedirectToAction("RoomSettings", booking);
            }

            return View();
        }

        [Authorize]
        public ActionResult RoomSettings(BookingModel booking)
        {
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomModel>>(
                serviceRoom.GetFreeRooms(booking.EnterDate, booking.LeaveDate));
            ViewBag.Rooms = rooms;
            return View(booking);
        }

        [Authorize]
        public ActionResult GuestSettings(DateTime enterDate, DateTime leaveDate, dynamic bookingRoom)
        {
            var rooms = mapperGuest.Map<IEnumerable<GuestDTO>, List<GuestModel>>(
                serviceGuest.GetAllGuests());
            var roomId = Convert.ToInt32(bookingRoom[0]);
            ViewBag.Guests = rooms;
            ViewBag.enterDate = enterDate;
            ViewBag.leaveDate = leaveDate;
            ViewBag.bookingRoom = roomId;

            return View();
        }

        [Authorize]
        public ActionResult Create(DateTime enterDate, DateTime leaveDate,
            dynamic bookingRoom, dynamic bookingGuest)
        {
            var roomId = Convert.ToInt32(bookingRoom[0]);
            var guestId = Convert.ToInt32(bookingGuest[0]);
            var userId = Convert.ToInt32(User.Identity.Name);
            var modelDTO = new BookingDTO()
            {
                UserId = userId,
                ActionUserId = userId,
                EnterDate = enterDate,
                LeaveDate = leaveDate,
                BookingRoom = new RoomDTO()
                { Id = roomId },
                BookingGuest = new GuestDTO()
                { Id = guestId }
            };

            service.Create(modelDTO);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var guests = mapperGuest.Map<IEnumerable<GuestDTO>, List<GuestModel>>(
                serviceGuest.GetAllGuests());
            var rooms = mapperRoom.Map<IEnumerable<RoomDTO>, List<RoomModel>>(
                serviceRoom.GetAllRooms());
            SelectList guestsList = new SelectList(guests, "Id", "FullName");
            SelectList roomsList = new SelectList(rooms, "Id", "Name");
            ViewBag.Guests = guestsList;
            ViewBag.Rooms = roomsList;

            var data = mapper.Map<BookingDTO, BookingModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                var modelDTO = Helpers.Mapper.MapToBookingDTO(model);
                service.Update(modelDTO.Id, modelDTO);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Model is invalid");

            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult CheckIn(int id)
        {
            service.CheckIn(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult CheckOut(int id)
        {
            service.CheckOut(id);
            return RedirectToAction("Index");
        }

        [Authorize]
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