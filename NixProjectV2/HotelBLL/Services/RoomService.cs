using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Services
{
    public class RoomService : IRoomService
    {
        private IWorkUnit Database { get; set; }

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Room, RoomDTO>()
            ).CreateMapper();

            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Room, RoomDTO>()
            ).CreateMapper();

            return mapper.Map<Room, RoomDTO>(Database.Rooms.Get(id));
        }
    }
}
