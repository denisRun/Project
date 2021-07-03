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


        public IEnumerable<RoomModel> Get()
        {
            var data = service.GetAllRooms();
            var rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(data);

            return rooms;
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            RoomDTO data = service.Get(id);
            var room = new RoomModel();

            if (data != null)
            {
                room = mapper.Map<RoomDTO, RoomModel>(data);
                return request.CreateResponse(HttpStatusCode.OK, room);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
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
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public void Put(int id, [FromBody] RoomModel value)
        {
        }


        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
