using Raven.Client;
using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase
{
    public class BookmarkRepository : EntityRepository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(IDataContextFactory<IDocumentSession> dataContextFactory)
            : base(dataContextFactory)
        {

        }
    }
}