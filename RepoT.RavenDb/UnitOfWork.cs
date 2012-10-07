using Raven.Client;
using RepoT.Infrastructure;

namespace RepoT.RavenDb
{
    public class UnitOfWork : UnitOfWorkBase<IDocumentSession>
    {
        public UnitOfWork(IDatabaseFactory<IDocumentSession> databaseFactory)
            : base(databaseFactory)
        {
        }

        public override void Commit()
        {
            if (DataContext != null) DataContext.SaveChanges();
        }
    }
}
