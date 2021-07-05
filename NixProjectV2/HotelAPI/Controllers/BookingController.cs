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

        [ResponseType(typeof(IEnumerable<BookingModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var data = service.GetAllBookings();

            if (data != null)
            {
                var bookings = mapper.Map<IEnumerable<BookingDTO>, List<BookingModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, bookings);
            }

            return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                BookingDTO data = service.Get(id);

                if (data != null)
                {
                    var booking = mapper.Map<BookingDTO, BookingModel>(data);
                    return request.CreateResponse(HttpStatusCode.OK, booking);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
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
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        public HttpResponseMessage Put(HttpRequestMessage request, int id,
            [FromBody] BookingModel value)
        {
            try
            {
                var booking = service.Get(id);

                if (booking != null)
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

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("api/Booking/MoneyPerMonth")]
        [HttpGet]
        public HttpResponseMessage MoneyPerMonthGet(HttpRequestMessage request)
        {
            try
            {
                var bookings = service.GetAllBookings();

                if (bookings != null)
                {
                    Dictionary<string, decimal> money = new Dictionary<string, decimal>();

                    foreach (var booking in bookings)
                    {
                        string key = booking.EnterDate.ToString("yyyy.MM");
                        decimal sum = Decimal.Parse((booking.LeaveDate - booking.EnterDate).TotalDays.ToString()) * booking.BookingRoom.RoomCategory.Price;

                        if (money.ContainsKey(key))
                            money[key] += sum;
                        else
                            money.Add(key, sum);
                    }

                    return request.CreateResponse(HttpStatusCode.OK, money);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [Route("api/Booking/Registration/{id}")]
        [HttpPut]
        public HttpResponseMessage RegistrationPut(HttpRequestMessage request, int id)
        {
            try
            {
                var booking = service.Get(id);

                if (booking != null)
                {
                    booking.Set = "yes";
                    service.Update(id, booking);
                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var booking = service.Get(id);

                if (booking != null)
                {
                    service.Delete(id);
                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
    }
}
