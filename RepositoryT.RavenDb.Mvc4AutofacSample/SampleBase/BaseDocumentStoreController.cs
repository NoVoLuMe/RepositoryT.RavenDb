using System.Web.Mvc;
using Raven.Client;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase
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
            DependencyResolver.Current.GetService<IDocumentSession>().SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}