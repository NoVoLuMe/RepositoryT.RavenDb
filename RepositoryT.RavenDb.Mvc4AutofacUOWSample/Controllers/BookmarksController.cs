using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.Models;
using RepositoryT.RavenDb.Mvc4AutofacUOWSample.SampleBase;

namespace RepositoryT.RavenDb.Mvc4AutofacUOWSample.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        public ViewResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetAll().OrderByDescending(item => item.DateCreated).ToList();
            return View(model);
        }

        public ViewResult Details(string id)
        {
            Bookmark model = _bookmarkService.GetById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new Bookmark();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Bookmark bookmark)
        {
            bookmark.DateCreated = DateTime.UtcNow;
            _bookmarkService.Add(bookmark);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var model = _bookmarkService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Bookmark bookmark)
        {
            _bookmarkService.Add(bookmark);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var model = _bookmarkService.GetById(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _bookmarkService.Delete(id);
            return RedirectToAction("Index");
        }

        public ViewResult Tag(string tag)
        {
            var model = new BookmarksByTagViewModel
                            {
                                Tag = tag,
                                Bookmarks =
                                    _bookmarkService.GetMany(item => item.Tags.Any(t => t == tag)).OrderByDescending(
                                        i => i.DateCreated).
                                    ToList()
                            };

            return View(model);
        }
    }
}