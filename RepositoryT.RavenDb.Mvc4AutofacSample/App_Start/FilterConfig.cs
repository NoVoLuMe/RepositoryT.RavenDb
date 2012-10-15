using System.Web.Mvc;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}