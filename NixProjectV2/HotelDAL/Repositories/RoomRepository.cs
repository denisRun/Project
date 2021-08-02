using HotelDAL.EF;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Repositories
{
    class RoomRepository : IRepository<Room>
    {
        private HotelContext db;

        public RoomRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            db.Rooms.Add(room);
        }

        public void Delete(int id)
        {
            Room room = Get(id);
            if (room != null)
                db.Rooms.Remove(room);
        }

        public void Update(int roomId, Room value)
        {
            var room = db.Rooms.FirstOrDefault(m => m.Id == roomId);
            if (room != null)
            {
                room.CategoryId = value.CategoryId > 0 ? value.CategoryId : room.CategoryId;
                room.Name = value.Name ?? room.Name;
                room.ActionTime = value.ActionTime != null ? value.ActionTime : room.ActionTime;
                room.ActionType = value.ActionType ?? room.ActionType;
                room.ActionUserId = value.ActionUserId > 0 ? value.ActionUserId : room.ActionUserId;
            }

        }
    }
}
