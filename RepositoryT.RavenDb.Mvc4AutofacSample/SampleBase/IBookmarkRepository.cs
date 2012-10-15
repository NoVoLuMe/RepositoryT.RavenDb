using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.SampleBase
{
    public interface IBookmarkRepository : IRepository<Bookmark>
    {
    }
}