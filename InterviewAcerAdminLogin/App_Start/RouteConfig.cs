using System.Web.Mvc;
using System.Web.Routing;

namespace InterviewAcerAdminLogin
{
    public class RouteConfig
    {
        public const string LoginRouteName = "LogIn";



        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(LoginRouteName, "log-in", new { controller = "Login", Action = "Index" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
