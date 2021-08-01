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
    public class GuestController : Controller
    {
        IGuestService service;
        IMapper mapper;

        public GuestController(IGuestService service)
        {
            this.service = service;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
        }
        // GET: Category
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(service.GetAllGuests());

            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = mapper.Map<GuestDTO, GuestModel>(service.Get(id));

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GuestModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDTO = mapper.Map<GuestModel, GuestDTO>(model);
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
            var data = mapper.Map<GuestDTO, GuestModel>(service.Get(id));

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(GuestModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDTO = mapper.Map<GuestModel, GuestDTO>(model);
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