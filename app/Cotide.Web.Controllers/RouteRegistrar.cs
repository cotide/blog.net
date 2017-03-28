using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cotide.Web.Controllers
{
    public class RouteRegistrar
    {

        public static void RegisterRoutesTo(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
             "Root",
             "",
             new
             {
                 controller = "Blog",
                 action = "Index",
                 domain = "xcli",
                 id = UrlParameter.Optional
             }
           );
 

            routes.MapRoute(
             "Index",
             "{domain}/Blog/Articles/{year}/{mouth}",
             new
             {
                 controller = "Blog",
                 action = "Articles",
                 year = UrlParameter.Optional,
                 mouth = UrlParameter.Optional
             }
         );


            routes.MapRoute(
                "UserBlog",
                "{domain}/Blog/{action}/{id}",
                new
                {
                    controller = "Blog",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );


            routes.MapRoute(
                "BlogManager",
                "{domain}/BlogManager/{action}/{id}",
                new
                {
                    controller = "BlogManager",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );



            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
             );

        }
    }
}
