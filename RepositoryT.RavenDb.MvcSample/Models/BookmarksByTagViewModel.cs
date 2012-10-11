using System.Collections.Generic;

namespace RepositoryT.RavenDb.MvcSample.Models
{
    public class BookmarksByTagViewModel
    {
        public string Tag { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
    }
}