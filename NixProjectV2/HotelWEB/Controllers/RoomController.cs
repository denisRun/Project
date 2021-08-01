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
    public class RoomController : Controller
    {
        IRoomService service;
        IMapper mapper;

        public RoomController(IRoomService service)
        {
            this.service = service;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
        }
        // GET: Room
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(service.GetAllRooms());

            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = mapper.Map<RoomDTO, RoomModel>(service.Get(id));

            return View(data);
        }
    }
}