using Raven.Client;
using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.MvcSample.Models;

namespace RepositoryT.RavenDb.MvcSample.SampleBase
{
    public class BookmarkRepository : EntityRepository<Bookmark>
    {
        public BookmarkRepository(IDataContextFactory<IDocumentSession> dataContextFactory) : base(dataContextFactory)
        {

        }
    }
}