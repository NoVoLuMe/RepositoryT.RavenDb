using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepoT.RavenDb.MvcSample.Models
{
    public class BookmarkModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;
            var tagsAsString = form["TagsAsString"];
            var bookmark = bindingContext.Model as Bookmark;
            bookmark.Tags = string.IsNullOrEmpty(tagsAsString)
                    ? new List<string>()
                    : tagsAsString.Split(',').Select(i => i.Trim()).ToList();
        }
    }
}