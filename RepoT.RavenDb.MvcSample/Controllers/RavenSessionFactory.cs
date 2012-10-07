using Raven.Client;
using Raven.Client.Embedded;
using RepoT.Infrastructure;

namespace RepoT.RavenDb.MvcSample.Controllers
{
    public class RavenSessionFactory : IDataContextFactory<IDocumentSession>
    {
        private static IDocumentStore _store;

        public IDocumentSession GetContext()
        {
            if (_store == null)
            {
                Init();
            }

            return _store.OpenSession();
        }

        public void Dispose()
        {
        }

        public static void Init()
        {
            var instance = new EmbeddableDocumentStore
                              {
                                  ConnectionStringName = "RavenDB"
                                      //,UseEmbeddedHttpServer = true
                                  ,
                                  Conventions = { IdentityPartsSeparator = "-" }
                              };
            instance.Initialize();
            _store = instance;
        }
    }
}