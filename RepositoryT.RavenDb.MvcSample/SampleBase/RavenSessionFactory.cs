using Raven.Client;
using Raven.Client.Embedded;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb.MvcSample.SampleBase
{
    public class RavenSessionFactory : IDataContextFactory<IDocumentSession>
    {
        private static IDocumentStore _store;
        private static IDocumentSession _currentSession;

        public IDocumentSession GetContext()
        {
            if (_store == null)
            {
                Init();
            }
            _currentSession = _store.OpenSession();
            return _currentSession;
        }

        public void Dispose()
        {
            if (_store != null)
                _store.Dispose();
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

        public static void DisposeSession()
        {
            if (_currentSession != null)
                _currentSession.Dispose();
        }
    }
}