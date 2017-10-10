using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DateCalculator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "DateCalculator",
               url: "DateCalculator/{action}/{id}",
               defaults: new { controller = "DateCalculator", action = "DateCalculator", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "AgeCalculator",
                url: "DateCalculator/{action}/{id}",
                defaults: new { controller = "DateCalculator", action = "AgeCalculator", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "BussinessCalculator",
              url: "DateCalculator/{action}/{id}",
              defaults: new { controller = "DateCalculator", action = "BussinessCalculator", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "Error",
              url: "ErrorController/Index",
              defaults: new { controller = "Error", action = "Index" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DateCalculator", action = "DateCalculator", id = UrlParameter.Optional }
            );
        }
    }
}