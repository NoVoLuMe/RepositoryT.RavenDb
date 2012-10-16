using Raven.Client;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase
{
    public class RavenSessionFactory : IDataContextFactory<IDocumentSession>
    {
        private readonly IDocumentStore _store;
        private IDocumentSession _currentSession;

        public RavenSessionFactory(IDocumentStore store)
        {
            _store = store;
        }

        public IDocumentSession GetContext()
        {
            if (_currentSession != null)
                return _currentSession;

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