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
            return mapperModelToDto.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public UserDTO Get(int id)
        {
            return mapperModelToDto.Map<User, UserDTO>(Database.Users.Get(id));
        }

        public void Create(UserDTO category)
        {
            var data = mapperDtoToModel.Map<UserDTO, User>(category);
            Database.Users.Create(data);
            Database.Save();
        }

        public void Update(int id, UserDTO category)
        {
            var data = mapperDtoToModel.Map<UserDTO, User>(category);
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
