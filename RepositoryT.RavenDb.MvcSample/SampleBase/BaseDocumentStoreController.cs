using System.Web.Mvc;
using Raven.Client;

namespace RepositoryT.RavenDb.MvcSample.SampleBase
{
    public class BaseDocumentStoreController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }

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