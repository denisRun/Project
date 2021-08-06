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
        IMapper mapperToDTO;

        public GuestController(IGuestService service)
        {
            this.service = service;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
            this.mapperToDTO = new MapperConfiguration(cfg =>
               cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
        }

        [Authorize]
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(
                service.GetAllGuests());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = mapper.Map<GuestDTO, GuestModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(GuestModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Length of Name must be at least 2");
                }
                if (model.Surname.Length < 2)
                {
                    ModelState.AddModelError("Surname", "Length of Surname must be at least 2");
                }

                if (ModelState.IsValid)
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = mapperToDTO.Map<GuestModel, GuestDTO>(model);
                    service.Create(modelDTO);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Model is invalid");

            return View(model);
        }

        [Authorize]
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
                if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Length of Name must be at least 2");
                }
                if (model.Surname.Length < 2)
                {
                    ModelState.AddModelError("Surname", "Length of Surname must be at least 2");
                }

                if (ModelState.IsValid)
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = mapperToDTO.Map<GuestModel, GuestDTO>(model);
                    service.Update(modelDTO.Id, modelDTO);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Model is invalid");

            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}