using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Services;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using HotelTests.TestDataHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelTests.ServicesTest
{
    [TestClass]
    public class CategoryServiceTest
    {
        Mock<IWorkUnit> EFWorkUnitMock;
        IMapper mapper;
        List<Category> categories;

        public CategoryServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            categories = TestData.CategoryList;
        }

        [TestMethod]
        public void CategoryGetAlCategorieslTest()
        {
            EFWorkUnitMock.Setup(a => a.Categories.GetAll()).Returns(categories);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            var result = categoryService.GetAllCategories().ToList();
            var expexted = mapper.Map<List<Category>, List<CategoryDTO>>(categories);

            CollectionAssert.AreEqual(expexted, result);
        }

        [TestMethod]
        public void CategoryGetTest()
        {
            int id = 1;
            EFWorkUnitMock.Setup(a => a.Categories.Get(id)).Returns(categories[id - 1]);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            var result = categoryService.Get(id);
            var expected = mapper.Map<Category, CategoryDTO>(categories[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CategoryCreateTest()
        {
            var category = new CategoryDTO()
            {
                Id = 9,
                Name = "Category9",
                Price = 9,
                Bed = 9
            };

            EFWorkUnitMock.Setup(a => a.Categories.Create(mapper.Map<CategoryDTO, Category>(category)));
            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            
            categoryService.Create(category);
            EFWorkUnitMock.Verify(a => a.Categories.Create(mapper.Map<CategoryDTO, Category>(category)));
        }

        [TestMethod]
        public void CategoryUpdateTest()
        {
            var id = 1;
            var category = categories[id - 1];
            var categoryDTO = mapper.Map<Category, CategoryDTO>(category);

            EFWorkUnitMock.Setup(a => a.Categories.Update(id, category));
            var categoryService = new CategoryService(EFWorkUnitMock.Object);

            categoryService.Update(id, categoryDTO);
            EFWorkUnitMock.Verify(a => a.Categories.Update(id, category));
        }

        [TestMethod]
        public void CategoryDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(a => a.Categories.Delete(id));
            var categoryService = new CategoryService(EFWorkUnitMock.Object);

            categoryService.Delete(id);
            EFWorkUnitMock.Verify(a => a.Categories.Delete(id));
        }
    }
}
