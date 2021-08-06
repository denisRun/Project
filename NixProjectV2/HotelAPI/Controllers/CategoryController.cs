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

        [ResponseType(typeof(IEnumerable<CategoryModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var data = service.GetAllCategories();

            if (data != null)
            {
                var categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, categories);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            if (id < 1)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                CategoryDTO data = service.Get(id);

                if (data != null)
                {
                    var category = mapper.Map<CategoryDTO, CategoryModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, category);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CategoryModel value)
        {
            try
            {
                var mapperToDTO = new MapperConfiguration(cfg =>
                    cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
                var data = mapperToDTO.Map<CategoryModel, CategoryDTO>(value);

                service.Create(data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id,
            [FromBody] CategoryModel value)
        {
            try
            {
                var category = service.Get(id);

                if (category != null)
                {
                    var mapper = new MapperConfiguration(cfg =>
                        cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
                    var data = mapper.Map<CategoryModel, CategoryDTO>(value);
                    service.Update(id, data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var category = service.Get(id);

                if (category != null)
                {
                    service.Delete(id);
                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}
