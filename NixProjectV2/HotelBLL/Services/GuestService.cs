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
    public class GuestService : IGuestService
    {
        private IWorkUnit Database { get; set; }
        private IMapper mapperModelToDto;
        private IMapper mapperDtoToModel;

        public GuestService(IWorkUnit database)
        {
            this.Database = database;
            mapperModelToDto = new MapperConfiguration(cfg =>
                cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
            mapperDtoToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<GuestDTO, Guest>()).CreateMapper();
        }

        public IEnumerable<GuestDTO> GetAllGuests()
        {
            return mapperModelToDto.Map<IEnumerable<Guest>, List<GuestDTO>>(Database.Guests.GetAll());
        }

        public GuestDTO Get(int id)
        {
            return mapperModelToDto.Map<Guest, GuestDTO>(Database.Guests.Get(id));
        }

        public void Create(GuestDTO guest)
        {
            var data = mapperDtoToModel.Map<GuestDTO, Guest>(guest);
            Database.Guests.Create(data);
            Database.Save();
        }

        public void Update(int id, GuestDTO guest)
        {
            var data = mapperDtoToModel.Map<GuestDTO, Guest>(guest);
            Database.Guests.Update(id, data);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Guests.Delete(id);
            Database.Save();
        }
    }
}
