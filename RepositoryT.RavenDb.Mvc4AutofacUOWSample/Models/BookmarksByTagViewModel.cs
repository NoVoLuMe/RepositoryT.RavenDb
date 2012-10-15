using System.Collections.Generic;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models
{
    public class BookmarksByTagViewModel
    {
        public string Tag { get; set; }
        public List<Bookmark> Bookmarks { get; set; }
    }
}