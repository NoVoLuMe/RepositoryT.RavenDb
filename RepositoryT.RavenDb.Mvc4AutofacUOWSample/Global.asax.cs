using System.Web.Mvc;
using System.Web.Routing;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.App_Start;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Initialize();

            ModelBinders.Binders.Add(typeof(Bookmark), new BookmarkModelBinder());
        }
    }
}