using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO Get(int id);
        void Create(UserDTO user);
        void Update(int id, UserDTO user);
        void Delete(int id);
    }
}
