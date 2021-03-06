using Raven.Client;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase
{
    public class RavenSessionFactory : IDataContextFactory<IDocumentSession>
    {
        public RavenSessionFactory(IDocumentStore store)
        {
            _store = store;
        }
        private readonly IDocumentStore _store;
        private IDocumentSession _currentSession;

        public IDocumentSession GetContext()
        {
            _currentSession = _store.OpenSession();
            return _currentSession;
        }

        public void Dispose()
        {
            if (_currentSession != null)
            {
                _currentSession.Dispose();
            }
        }
    }
}