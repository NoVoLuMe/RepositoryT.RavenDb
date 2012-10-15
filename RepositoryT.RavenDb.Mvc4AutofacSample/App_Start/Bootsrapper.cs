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
    public class Bootsrapper
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.Register<IDocumentStore>(x => new EmbeddableDocumentStore
                {
                    ConnectionStringName = ConfigurationManager.AppSettings["RavenConnStr"]
                        //,UseEmbeddedHttpServer = true
                ,
                    Conventions = { IdentityPartsSeparator = "-" }
                }.Initialize()).SingleInstance();

            // Register ISessionFactory as Singleton 
            builder.Register<IDataContextFactory<IDocumentSession>>(x =>
                new RavenSessionFactory(x.Resolve<IDocumentStore>()))
                .InstancePerHttpRequest();
            ////Register IDocumentSession as instance per web request
            //builder.Register(x => x.Resolve<IDataContextFactory<IDocumentSession>>().GetContext()).InstancePerHttpRequest();

            builder.Register<IBookmarkRepository>(x => new BookmarkRepository(x.Resolve<IDataContextFactory<IDocumentSession>>()));
            builder.Register<IBookmarkService>(x => new BookmarkService(x.Resolve<IBookmarkRepository>()));

            //Register all controllers
            builder.RegisterAssemblyTypes(typeof(BookmarksController).Assembly)
                .InNamespaceOf<BookmarksController>()
                .AsSelf();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}