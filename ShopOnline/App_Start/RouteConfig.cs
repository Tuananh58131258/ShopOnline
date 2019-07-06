using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "HomePage", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Home",
               url: "HomePage",
               defaults: new { controller = "HomePage", action = "HomePage", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Add Cart",
             url: "them-gio-hang",
             defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
             namespaces: new[] {"ShopOnline.Controllers"}
         );
        }
    }
}
