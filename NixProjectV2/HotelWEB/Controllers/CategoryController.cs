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
    }
}