using System.Web.Mvc;
using Raven.Client;

namespace RepoT.RavenDb.MvcSample.Controllers
{
    public class BaseDocumentStoreController : Controller
    {
        public IDocumentSession DocumentSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;
            //DocumentSession = DataDocumentStore.Instance.OpenSession();
            //DocumentSession = GetSession();
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
            //if (DocumentSession != null && filterContext.Exception == null)
            //    DocumentSession.SaveChanges();
            //IDocumentSession documentSession = DocumentSession;
            //if (documentSession != null) documentSession.Dispose();
            base.OnActionExecuted(filterContext);
        }
    }
}