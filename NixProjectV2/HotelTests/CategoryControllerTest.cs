using HotelAPI.Controllers;
using HotelAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using Moq;
using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using Newtonsoft.Json;
using HotelDAL.Interfaces;
using HotelDAL.Entities;
using HotelBLL.Services;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Collections.Generic;
using System.Net;

namespace HotelTests
{
    [TestClass]
    public class CategoryControllerTest
    {
        [TestMethod]
        public void CategoryGetByIdTypeIsCategoryModel()
        {
            int id = 1;

            var CategoryServiceMoq = new Mock<ICategoryService>();
            CategoryServiceMoq.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            Assert.IsInstanceOfType(result.Result, typeof(CategoryModel));
        }

        [TestMethod]
        public void CategoryGetByIdNegative()
        {

            int id = -1;

            var EFWorkUnitMoq = new Mock<IWorkUnit>();
            var CategoryServiceMoq = new Mock<ICategoryService>();
            EFWorkUnitMoq.Setup(a => a.Categories.Get(id)).Returns(new Category());
            CategoryServiceMoq.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var categoryService = new CategoryService(EFWorkUnitMoq.Object);
            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request, id);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void CategoryGetTypeIsCategoryModel()
        {
            var CategoryServiceMoq = new Mock<ICategoryService>();
            CategoryServiceMoq.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();

            Assert.IsInstanceOfType(result.Result, typeof(List<CategoryModel>));
        }

        [TestMethod]
        public void CategoryGetByIdIsNotNull()
        {
            int id = 1;

            var EFWorkUnitMoq = new Mock<IWorkUnit>();
            var CategoryServiceMoq = new Mock<ICategoryService>();
            EFWorkUnitMoq.Setup(a => a.Categories.Get(id)).Returns(new Category());
            CategoryServiceMoq.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var categoryService = new CategoryService(EFWorkUnitMoq.Object);
            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void CategoryGetAllIsNotNull()
        {
            var EFWorkUnitMoq = new Mock<IWorkUnit>();
            var CategoryServiceMoq = new Mock<ICategoryService>();
            EFWorkUnitMoq.Setup(a => a.Categories.GetAll()).Returns(new List<Category>());
            CategoryServiceMoq.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            var categoryService = new CategoryService(EFWorkUnitMoq.Object);
            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();
           
            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void CategoryGetByIdTest()
        {
            int id = 1;

            var EFWorkUnitMoq = new Mock<IWorkUnit>();
            var CategoryServiceMoq = new Mock<ICategoryService>();
            EFWorkUnitMoq.Setup(a => a.Categories.Get(id)).Returns(new Category());
            CategoryServiceMoq.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var categoryService = new CategoryService(EFWorkUnitMoq.Object);
            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()
            ).CreateMapper();
            CategoryModel expected = mapper.Map<CategoryDTO, CategoryModel>(categoryService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }



        [TestMethod]
        public void CategoryGetAllTest()
        {
            var EFWorkUnitMoq = new Mock<IWorkUnit>();
            var CategoryServiceMoq = new Mock<ICategoryService>();
            EFWorkUnitMoq.Setup(a => a.Categories.GetAll()).Returns(new List<Category>());
            CategoryServiceMoq.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            var categoryService = new CategoryService(EFWorkUnitMoq.Object);
            CategoryController controller = new CategoryController(CategoryServiceMoq.Object);

            HttpRequestMessage request = new HttpRequestMessage();
            HttpConfiguration config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<IEnumerable<CategoryDTO>, List<CategoryModel>>()
            ).CreateMapper();

            List<CategoryModel> expected = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryService.GetAllCategories());

            CollectionAssert.AreEqual(expected, result.Result);
        }
    }
}
