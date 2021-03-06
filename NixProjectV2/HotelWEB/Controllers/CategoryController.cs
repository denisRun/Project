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
    public class CategoryController : Controller
    {
        ICategoryService service;
        IMapper mapper;
        IMapper mapperToDTO;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            this.mapperToDTO = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
        }

        [Authorize]
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(
                service.GetAllCategories());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = mapper.Map<CategoryDTO, CategoryModel>(
                service.Get(id));
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
        public ActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Price <= 0)
                {
                    ModelState.AddModelError("Price",
                        "Price must be positive");
                }
                if (model.Bed <= 0)
                {
                    ModelState.AddModelError("Bed",
                        "Amount of places must be positive");
                }

                if (ModelState.IsValid)
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = mapperToDTO.Map<CategoryModel, CategoryDTO>(model);
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
            var data = mapper.Map<CategoryDTO, CategoryModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Price <= 0)
                {
                    ModelState.AddModelError("Price",
                        "Price must be positive");
                }
                if (model.Bed <= 0)
                {
                    ModelState.AddModelError("Bed",
                        "Amount of places must be positive");
                }

                if (ModelState.IsValid)
                {
                    model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                    var modelDTO = mapperToDTO.Map<CategoryModel, CategoryDTO>(model);
                    service.Update(modelDTO.Id, modelDTO);
                    return RedirectToAction("Index");
                }
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
    }
}