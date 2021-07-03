using AutoMapper;
using HotelAPI.Models;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HotelAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private ICategoryService service;
        private IMapper mapper;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }


        public IEnumerable<CategoryModel> Get()
        {
            var data = service.GetAllCategories();
            var categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);

            return categories;
        }

        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            CategoryDTO data = service.Get(id);
            var category = new CategoryModel();

            if (data != null)
            {
                category = mapper.Map<CategoryDTO, CategoryModel>(data);
                return request.CreateResponse(HttpStatusCode.OK, category);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CategoryModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
                var data = mapper.Map<CategoryModel, CategoryDTO>(value);
                service.Create(data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public void Put(int id, [FromBody] CategoryModel value)
        {
        }


        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
