using AutoMapper;
using HotelAPI.Models;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HotelAPI.Controllers
{
    public class BookingController : ApiController
    {
        private IBookingService service;
        private IMapper mapper;

        public BookingController(IBookingService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();
        }


        public IEnumerable<BookingModel> Get()
        {
            var data = service.GetAllBookings();
            var bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(data);

            return bookings;
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            BookingDTO data = service.Get(id);
            var booking = new BookingModel();

            if (data != null)
            {
                booking = mapper.Map<BookingDTO, BookingModel>(data);
                return request.CreateResponse(HttpStatusCode.OK, booking);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] BookingModel value)
        {
            try
            {
                var data = new BookingDTO()
                {
                    BookingGuest = new GuestDTO()
                    {
                        Id = value.BookingGuest.Id,
                        Name = value.BookingGuest.Name,
                        Surname = value.BookingGuest.Surname
                    },
                    BookingRoom = new RoomDTO()
                    {
                        Id = value.BookingRoom.Id,
                        Name = value.BookingRoom.Name,
                        RoomCategory = new CategoryDTO()
                        {
                            Id = value.BookingRoom.RoomCategory.Id,
                            Name = value.BookingRoom.RoomCategory.Name,
                            Price = value.BookingRoom.RoomCategory.Price,
                            Bed = value.BookingRoom.RoomCategory.Bed
                        }
                    },
                    BookingDate = value.BookingDate,
                    EnterDate = value.EnterDate,
                    LeaveDate = value.LeaveDate,
                    Set = value.Set
                };
                service.Create(data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(ex.Message);
            }  
        }



        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id,
            [FromBody] BookingModel value)
        {
            try
            {
                var data = new BookingDTO()
                {
                    BookingGuest = new GuestDTO()
                    {
                        Id = value.BookingGuest.Id,
                        Name = value.BookingGuest.Name,
                        Surname = value.BookingGuest.Surname
                    },
                    BookingRoom = new RoomDTO()
                    {
                        Id = value.BookingRoom.Id,
                        Name = value.BookingRoom.Name,
                        RoomCategory = new CategoryDTO()
                        {
                            Id = value.BookingRoom.RoomCategory.Id,
                            Name = value.BookingRoom.RoomCategory.Name,
                            Price = value.BookingRoom.RoomCategory.Price,
                            Bed = value.BookingRoom.RoomCategory.Bed
                        }
                    },
                    BookingDate = value.BookingDate,
                    EnterDate = value.EnterDate,
                    LeaveDate = value.LeaveDate,
                    Set = value.Set
                };
                service.Update(id, data);
                return request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
