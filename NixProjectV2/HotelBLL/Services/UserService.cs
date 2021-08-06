using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Helpers;
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
    public class UserService : IUserService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperModelToDto;
        private IMapper mapperDtoToModel;

        public UserService(IWorkUnit database)
        {
            this.Database = database;
            mapperModelToDto = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserDTO>()).CreateMapper();
            mapperDtoToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<User, UserDTO>()).CreateMapper();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return mapperModelToDto.Map<IEnumerable<User>, List<UserDTO>>(
                Database.Users.GetAll());
        }

        public UserDTO Get(int id)
        {
            return mapperModelToDto.Map<User, UserDTO>(
                Database.Users.Get(id));
        }

        public UserDTO Login(UserDTO user)
        {
            user.Password = Crypto.Hash(user.Password);
            var users = Database.Users.GetAll();
            var result = users.FirstOrDefault(us =>
                us.Login == user.Login && us.Password == user.Password); 
            
            return mapperModelToDto.Map<User, UserDTO>(result);
        }

        public UserDTO Register(UserDTO user)
        {
            user.Password = Crypto.Hash(user.Password);
            var users = Database.Users.GetAll();
            var result = users.FirstOrDefault(us => us.Login == user.Login);

            if(result == null)
            {
                Database.Users.Create(
                    mapperDtoToModel.Map<UserDTO, User>(user));
                Database.Save();
            }

            return mapperModelToDto.Map<User, UserDTO>(result);
        }

        public void Create(UserDTO user)
        {
            user.Password = Crypto.Hash(user.Password);
            var data = mapperDtoToModel.Map<UserDTO, User>(user);

            Database.Users.Create(data);
            Database.Save();
        }

        public void Update(int id, UserDTO user)
        {
            var data = mapperDtoToModel.Map<UserDTO, User>(user);

            Database.Users.Update(id, data);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Users.Delete(id);
            Database.Save();
        }
    }
}
