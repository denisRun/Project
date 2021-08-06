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
        IMapper mapperCategory;

        public RoomController(IRoomService service, ICategoryService serviceCategory)
        {
            this.service = service;
            this.serviceCategory = serviceCategory;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
            this.mapperCategory = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

        [Authorize]
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(
                service.GetAllRooms());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = mapper.Map<RoomDTO, RoomModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        public ActionResult FreeRooms(DateTime startDate, DateTime endDate)
        {
            var data = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(
                service.GetFreeRooms(startDate, endDate));
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>,List<CategoryModel>>(
                serviceCategory.GetAllCategories());
            var categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(RoomModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Length of Name must be at least 2");
                }

                if (ModelState.IsValidField("Name"))
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = Helpers.Mapper.MapToRoomDTO(model);
                    service.Create(modelDTO);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Model is invalid");

            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(
                serviceCategory.GetAllCategories());
            SelectList categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList;

            return View(model);

        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(
                serviceCategory.GetAllCategories());
            var categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList;

            var data = mapper.Map<RoomDTO, RoomModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(RoomModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Name.Length < 2)
                {
                    ModelState.AddModelError("Name", "Length of Name must be at least 2");
                }

                if (ModelState.IsValidField("Name"))
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = Helpers.Mapper.MapToRoomDTO(model);
                    service.Update(modelDTO.Id, modelDTO);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Model is invalid");

            var categories = mapperCategory.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(
                serviceCategory.GetAllCategories());
            var categoriesList = new SelectList(categories, "Id", "Name");
            ViewBag.Categories = categoriesList; 
            
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