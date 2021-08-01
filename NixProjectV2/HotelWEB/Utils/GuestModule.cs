using HotelBLL.Interfaces;
using HotelBLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelWEB.Utils
{
    public class GuestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGuestService>().To<GuestService>();
        }
    }
}