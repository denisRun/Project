using HotelBLL.Interfaces;
using HotelBLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Utils
{
    public class BookingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookingService>().To<BookingService>();
        }
    }
}