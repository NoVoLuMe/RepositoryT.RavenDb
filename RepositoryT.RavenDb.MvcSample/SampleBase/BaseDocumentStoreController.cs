using System.Web.Mvc;

namespace RepositoryT.RavenDb.MvcSample.SampleBase
{
    public class BaseDocumentStoreController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            RavenSessionFactory.DisposeSession();

            base.OnActionExecuted(filterContext);
        }
    }
}