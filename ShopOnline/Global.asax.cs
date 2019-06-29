using ShopOnline.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShopOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Optimization.BundleTable.Bundles.Add(new System.Web.Optimization.ScriptBundle("~/bundle/js")
              .Include("~/Content/js/*.js"));
            System.Web.Optimization.BundleTable.Bundles.Add(new System.Web.Optimization.StyleBundle("~/bundle/css")
                 .Include("~/Content/css/*.css"));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
