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

        public GuestService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<GuestDTO> GetAllGuests()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Guest, GuestDTO>()
            ).CreateMapper();

            return mapper.Map<IEnumerable<Guest>, List<GuestDTO>>(Database.Guests.GetAll());
        }

        public GuestDTO Get(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Guest, GuestDTO>()
            ).CreateMapper();

            return mapper.Map<Guest, GuestDTO>(Database.Guests.Get(id));
        }
    }
}
