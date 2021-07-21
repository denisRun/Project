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
using HotelTests.TestDataHelper;

namespace HotelTests
{
    [TestClass]
    public class CategoryControllerTest
    {
        private Mock<ICategoryService> CategoryServiceMock;
        private Mock<IWorkUnit> EFWorkUnitMock;
        private IMapper mapper;
        private HttpRequestMessage request;
        private HttpConfiguration config;

        public CategoryControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            CategoryServiceMock = new Mock<ICategoryService>();
            request = new HttpRequestMessage();
            config = new HttpConfiguration();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryModel, CategoryDTO>()
            ).CreateMapper();
        }

        [TestMethod]
        public void CategoryGetByIdTypeIsCategoryModel()
        {
            int id = 1;
            var category = TestData.CategoryList[id - 1];
            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            Assert.IsInstanceOfType(result.Result, typeof(CategoryModel));
        }

        [TestMethod]
        public void CategoryGetByIdNegative()
        {
            int id = -1;

            EFWorkUnitMock.Setup(a => a.Categories.Get(id)).Returns(new Category());
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request, id);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void CategoryGetTypeIsCategoryModel()
        {
            CategoryServiceMock.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();

            Assert.IsInstanceOfType(result.Result, typeof(List<CategoryModel>));
        }

        [TestMethod]
        public void CategoryGetByIdIsNotNull()
        {
            int id = 1;
            var category = TestData.CategoryList[id - 1];
            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            EFWorkUnitMock.Setup(a => a.Categories.Get(id)).Returns(category);
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void CategoryGetAllIsNotNull()
        {
            EFWorkUnitMock.Setup(a => a.Categories.GetAll()).Returns(new List<Category>());
            CategoryServiceMock.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();
           
            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void CategoryGetByIdTest()
        {
            int id = 1;
            var category = TestData.CategoryList[id - 1];
            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            EFWorkUnitMock.Setup(a => a.Categories.Get(id)).Returns(category);
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request, id);
            var result = response.Content.ReadAsAsync<CategoryModel>();

            CategoryModel expected = mapper.Map<CategoryDTO, CategoryModel>(categoryService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }



        [TestMethod]
        public void CategoryGetAllTest()
        {
            EFWorkUnitMock.Setup(a => a.Categories.GetAll()).Returns(new List<Category>());
            CategoryServiceMock.Setup(a => a.GetAllCategories()).Returns(new List<CategoryDTO>());

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var response = controller.Get(request);
            var result = response.Content.ReadAsAsync<List<CategoryModel>>();

            List<CategoryModel> expected = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryService.GetAllCategories());

            CollectionAssert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void CategoryPostTest()
        {
            CategoryModel category = new CategoryModel()
            {
                Id = 8,
                Name = "Category8",
                Price = 8,
                Bed = 8
            };

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var response = controller.Post(request, category);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryPutTest()
        {
            var id = 1;
            var category = TestData.CategoryList[id - 1];

            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var response = controller.Put(request, id, mapper.Map<CategoryDTO, CategoryModel>(categoryDTO));
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryDeleteTest()
        {
            var id = 1;
            var category = TestData.CategoryList[id - 1];

            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryDeleteTestNegative()
        {
            var id = 1;
            Category category = null;

            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var response = controller.Delete(request, id);
            var result = response.StatusCode;

            Assert.AreNotEqual(HttpStatusCode.OK, result);
        }
    }
}
