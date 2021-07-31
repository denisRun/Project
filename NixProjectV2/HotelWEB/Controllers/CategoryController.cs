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

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            this.mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }
        // GET: Category
        public ActionResult Index()
        {
            var data = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(service.GetAllCategories());

            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = mapper.Map<CategoryDTO, CategoryModel>(service.Get(id));

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDTO = mapper.Map<CategoryModel, CategoryDTO>(model);
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
            var data = mapper.Map<CategoryDTO, CategoryModel>(service.Get(id));

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDTO = mapper.Map<CategoryModel, CategoryDTO>(model);
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