﻿using System.Web.Mvc;
using System.Web.Routing;
using RepositoryT.RavenDb.MvcSample.Models;
using RepositoryT.RavenDb.MvcSample.SampleBase;

namespace RepositoryT.RavenDb.MvcSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("BookmarkTag", "Bookmarks/Tag/{tag}", new { controller = "Bookmarks", action = "Tag", tag = "" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Bookmarks", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Bookmark), new BookmarkModelBinder());
            RavenSessionFactory.Init();
        }
    }
}