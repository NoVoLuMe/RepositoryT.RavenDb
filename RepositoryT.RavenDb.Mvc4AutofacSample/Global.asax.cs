using System.Web.Mvc;
using System.Web.Routing;
using RepositoryT.RavenDb.Mvc4AutofacSample.App_Start;
using RepositoryT.RavenDb.Mvc4AutofacSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bootsrapper.Initialize();

            ModelBinders.Binders.Add(typeof(Bookmark), new BookmarkModelBinder());
        }
    }
}