using System.Web.Mvc;
using Raven.Client;

namespace RepoT.RavenDb.MvcSample.SampleBase
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

        private IDocumentSession GetSession()
        {
            return new RavenSessionFactory().GetContext();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            base.OnActionExecuted(filterContext);
        }
    }
}