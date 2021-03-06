using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models
{
    public class BookmarkModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;
            var tagsAsString = form["TagsAsString"];
            var bookmark = bindingContext.Model as Bookmark;

            if (bookmark != null)
            {
                bookmark.Tags = string.IsNullOrEmpty(tagsAsString)
                                    ? new List<string>()
                                    : tagsAsString.Split(',').Select(i => i.Trim()).ToList();
            }
        }
    }
}