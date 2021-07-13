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
            var room = Database.Rooms.Get(id);
            return mapper.Map<Room, RoomDTO>(room);
        }

        public void Create(RoomDTO guest)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, Room>()
            ).CreateMapper();
            var data = mapper.Map<RoomDTO, Room>(guest);
            Database.Rooms.Create(data);
            Database.Save();
        }

        public void Update(int id, RoomDTO guest)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, Room>()
            ).CreateMapper();
            var data = mapper.Map<RoomDTO, Room>(guest);
            Database.Rooms.Update(id, data);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Rooms.Delete(id);
            Database.Save();
        }
    }
}
