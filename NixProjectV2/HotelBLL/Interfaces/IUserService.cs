using HotelBLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO Get(int id);
        UserDTO Login(UserDTO user);
        UserDTO Register(UserDTO user);
        void Create(UserDTO user);
        void Update(int id, UserDTO user);
        void Delete(int id);
    }
}
