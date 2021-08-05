using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();
        RoomDTO Get(int id);
        IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate);
        void Create(RoomDTO room);
        void Update(int id, RoomDTO room);
        void Delete(int id);
    }
}
