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
    public class CategoryService : ICategoryService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperModelToDto;
        private IMapper mapperDtoToModel;

        public CategoryService(IWorkUnit database)
        {
            this.Database = database;
            mapperModelToDto = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            mapperDtoToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return mapperModelToDto.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
        }

        public CategoryDTO Get(int id)
        {
            return mapperModelToDto.Map<Category, CategoryDTO>(Database.Categories.Get(id));
        }

        public void Create(CategoryDTO category)
        { 
            var data = mapperDtoToModel.Map<CategoryDTO, Category>(category);
            Database.Categories.Create(data);
            Database.Save();
        }

        public void Update(int id, CategoryDTO category)
        {
            var data = mapperDtoToModel.Map<CategoryDTO, Category>(category);
            Database.Categories.Update(id, data);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Categories.Delete(id);
            Database.Save();
        }
    }
}
