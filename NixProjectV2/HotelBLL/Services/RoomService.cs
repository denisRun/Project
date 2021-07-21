 
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
        private IMapper mapperModelToDto;
        private IMapper mapperDtoToModel;

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
            mapperModelToDto = new MapperConfiguration(cfg =>
                cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
            mapperDtoToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<RoomDTO, Room>()).CreateMapper();
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return mapperModelToDto.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            var room = Database.Rooms.Get(id);
            return mapperModelToDto.Map<Room, RoomDTO>(room);
        }

        public void Create(RoomDTO room)
        {
            var data = mapperDtoToModel.Map<RoomDTO, Room>(room);
            Database.Rooms.Create(data);
            Database.Save();
        }

        public void Update(int id, RoomDTO room)
        {
            var data = mapperDtoToModel.Map<RoomDTO, Room>(room);
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