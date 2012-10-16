using RepositoryT.Infrastructure;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase.Repository
{
    public interface IBookmarkRepository : IRepository<Bookmark>
    {
    }
}