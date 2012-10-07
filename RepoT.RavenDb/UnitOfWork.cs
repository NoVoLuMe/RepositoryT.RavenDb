using Raven.Client;
using RepoT.Infrastructure;

namespace RepoT.RavenDb
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
