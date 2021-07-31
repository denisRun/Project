using HotelBLL.Infrastructure;
using HotelWEB.Utils;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HotelWEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule categoryModule = new CategoryModule();
            NinjectModule bookingModule = new BookingModule();
            NinjectModule dependencyModule = new DependencyModule("HotelModel");
            var kernel = new StandardKernel(dependencyModule, categoryModule, bookingModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));
        }
    }
}