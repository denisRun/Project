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

        [ResponseType(typeof(IEnumerable<GuestModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var data = service.GetAllGuests();

            if (data != null)
            {
                var guests = mapper.Map<IEnumerable<GuestDTO>, List<GuestModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, guests);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [ResponseType(typeof(GuestModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
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
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] GuestModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
                var data = mapper.Map<GuestModel, GuestDTO>(value);

                if (data != null)
                {
                    service.Create(data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public HttpResponseMessage Put(HttpRequestMessage request, int id,
            [FromBody] GuestModel value)
        {
            try
            {
                var guest = service.Get(id);

                if (guest != null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
                    var data = mapper.Map<GuestModel, GuestDTO>(value);
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
                var guest = service.Get(id);

                if (guest != null)
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
