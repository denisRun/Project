
using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return mapperModelToDto.Map<IEnumerable<Room>, List<RoomDTO>>(
                Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            var room = Database.Rooms.Get(id);
            return mapperModelToDto.Map<Room, RoomDTO>(room);
        }

        public IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            var freeRooms = new List<Room>();
            var rooms = Database.Rooms.GetAll();
            var bookings = Database.Bookings.GetAll();
            var roomsInUse = bookings.Where(b => 
                (b.EnterDate <= startDate && b.LeaveDate > startDate) ||
                (b.EnterDate > startDate && b.EnterDate <= endDate)).
                Select(b => b.BookingRoom.Id);

            foreach (var room in rooms)
            {
                if (!roomsInUse.Contains(room.Id))
                {
                    freeRooms.Add(room);
                }
            }

            return mapperModelToDto.Map<IEnumerable<Room>, List<RoomDTO>>(
                freeRooms.Distinct());
        }

        public void Create(RoomDTO room)
        {
            string actionType = "Create";
            DateTime actionTime = DateTime.Now;
            room.ActionType = actionType;
            room.ActionTime = actionTime;
            var data = Helpers.Mapper.MapToRoom(room);

            Database.Rooms.Create(data);
            Database.Save();
        }

        public void Update(int id, RoomDTO room)
        {
            string actionType = "Update";
            DateTime actionTime = DateTime.Now;
            room.ActionType = actionType;
            room.ActionTime = actionTime;
            var data = Helpers.Mapper.MapToRoom(room);

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