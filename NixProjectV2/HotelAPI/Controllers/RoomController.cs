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
    public class RoomController : ApiController
    {
        private IRoomService service;
        private IMapper mapper;

        public RoomController(IRoomService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
        }

        [ResponseType(typeof(IEnumerable<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var data = service.GetAllRooms();

            if (data != null)
            {
                var rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, rooms);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                RoomDTO data = service.Get(id);

                if (data != null)
                {
                    var room = mapper.Map<RoomDTO, RoomModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, room);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] RoomModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
                var data = mapper.Map<RoomModel, RoomDTO>(value);

                service.Create(data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Put(HttpRequestMessage request, int id,
            [FromBody] RoomModel value)
        {
            try
            {
                var room = service.Get(id);

                if (room != null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
                    var data = mapper.Map<RoomModel, RoomDTO>(value);
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
                var room = service.Get(id);

                if (room != null)
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
