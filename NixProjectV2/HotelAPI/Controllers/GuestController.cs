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
    public class GuestController : ApiController
    {
        private IGuestService service;
        private IMapper mapper;

        public GuestController(IGuestService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
        }


        public IEnumerable<GuestModel> Get()
        {
            var data = service.GetAllGuests();
            var guests = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(data);

            return guests;
        }

        [ResponseType(typeof(GuestModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            GuestDTO data = service.Get(id);
            var guest = new GuestModel();

            if (data != null)
            {
                guest = mapper.Map<GuestDTO, GuestModel>(data);
                return request.CreateResponse(HttpStatusCode.OK, guest);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] GuestModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
                var data = mapper.Map<GuestModel, GuestDTO>(value);
                service.Create(data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public void Put(int id, [FromBody] GuestModel value)
        {
        }


        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
