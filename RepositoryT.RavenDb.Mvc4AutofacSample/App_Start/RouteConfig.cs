using System.Web.Mvc;
using System.Web.Routing;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("BookmarkTag", "Bookmarks/Tag/{tag}",
                new { controller = "Bookmarks", action = "Tag", tag = "" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Bookmarks", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}