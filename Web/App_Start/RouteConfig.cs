using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes
                .MapRoute(
                    "DefaultWithLanguage",
                    "{language}/{controller}/{action}/{id}",
                    new { language = "en", controller = "Home", action = "Index", id = UrlParameter.Optional },                    
                    new[] { "Web.Controllers" })
                .DataTokens["UseNamespaceFallback"] = false;

           

            routes
                .MapRoute(
                    "DefaultAreaWithLanguage",
                    "{area}/{language}/{controller}/{action}/{id}",
                    new { language = "en", controller = "Home", action = "Index", id = UrlParameter.Optional, area = "Admin" },                  
                    new[] { "Web.Areas.Admin.Controllers" })
                .DataTokens["UseNamespaceFallback"] = false;

            

        }
    }
}
