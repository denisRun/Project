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
        ICategoryService serviceCategory;
        IMapper mapper;
        IMapper mapperToDTO;
        IMapper mapperCategories;

        public RoomController(IRoomService service, ICategoryService serviceCategory)
        {
            this.service = service;
            this.serviceCategory = serviceCategory;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
            this.mapperToDTO = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
            this.mapperCategories = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
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

        public ActionResult FreeRooms()
        {
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(5);

            var data = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(
                service.GetFreeRooms(startDate,endDate));
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = mapperCategories.Map<IEnumerable<CategoryDTO>,
                List<CategoryModel>>(serviceCategory.GetAllCategories());
            SelectList categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(RoomModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                var modelDTO = new RoomDTO()
                {
                    Name = model.Name,
                    ActionUserId = model.ActionUserId,
                    RoomCategory = new CategoryDTO()
                    {
                        Id = model.RoomCategory.Id
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
            var categories = mapperCategories.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(serviceCategory.GetAllCategories());
            SelectList categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList;

            var data = mapper.Map<RoomDTO, RoomModel>(service.Get(id));
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(RoomModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                var modelDTO = new RoomDTO()
                {
                    Id=model.Id,
                    Name = model.Name,
                    ActionUserId = model.ActionUserId,
                    RoomCategory = new CategoryDTO()
                    {
                        Id = model.RoomCategory.Id
                    }
                };

                service.Update(modelDTO.Id, modelDTO);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Model is invalid");
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