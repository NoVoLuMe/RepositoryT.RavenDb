using Raven.Client;
using RepoT.Infrastructure;
using RepoT.RavenDb.MvcSample.Models;

namespace RepoT.RavenDb.MvcSample.Controllers
{
    public class BookmarkRepository : EntityRepository<Bookmark>
    {
        public BookmarkRepository(IDataContextFactory<IDocumentSession> dataContextFactory) : base(dataContextFactory)
        {

        }

        public override void Update(Bookmark entity)
        {
            
        }
    }
}