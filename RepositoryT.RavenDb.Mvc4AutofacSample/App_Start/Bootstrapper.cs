using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Embedded;
using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacSample.Controllers;
using RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.App_Start
{
    public class Bootstrapper
    {
        public static void Initialize()
        {
            InitAutofacDIResolver();
        }

        private static void InitAutofacDIResolver()
        {
            var builder = new ContainerBuilder();

            builder.Register<IDocumentStore>(x => new EmbeddableDocumentStore
                {
                    ConnectionStringName = ConfigurationManager.AppSettings["RavenConnStr"],
                    Conventions = {IdentityPartsSeparator = "-"}
                }.Initialize()).SingleInstance();

            builder.Register<IDataContextFactory<IDocumentSession>>(x =>
                                                                    new RavenSessionFactory(x.Resolve<IDocumentStore>()))
                .InstancePerHttpRequest();

            builder.Register<IBookmarkRepository>(
                x => new BookmarkRepository(x.Resolve<IDataContextFactory<IDocumentSession>>()));
            builder.Register<IBookmarkService>(x => new BookmarkService(x.Resolve<IBookmarkRepository>()));

            builder.RegisterAssemblyTypes(typeof (BookmarksController).Assembly)
                .InNamespaceOf<BookmarksController>()
                .AsSelf();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}