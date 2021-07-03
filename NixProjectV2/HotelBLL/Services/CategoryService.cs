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
    class CategoryService : ICategoryService
    {
        private IWorkUnit Database { get; set; }

        public CategoryService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<CategoryDTO> GetAllRooms()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, RoomDTO>()
            ).CreateMapper();

            return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()
            ).CreateMapper();

            return mapper.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }
    }
}
