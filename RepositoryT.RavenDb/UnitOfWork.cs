using Raven.Client;
using RepositoryT.Infrastructure;

namespace RepositoryT.RavenDb
{
    public class UnitOfWork : UnitOfWorkBase<IDocumentSession>
    {
        public UnitOfWork(IDataContextFactory<IDocumentSession> dataContextFactory)
            : base(dataContextFactory)
        {
        }

        public override void Commit()
        {
            if (DataContext != null) DataContext.SaveChanges();
        }
    }
}
