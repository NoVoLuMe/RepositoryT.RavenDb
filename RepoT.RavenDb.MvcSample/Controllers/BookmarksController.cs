using System;
using System.Linq;
using System.Web.Mvc;
using RepoT.RavenDb.MvcSample.Models;
using RepoT.RavenDb.MvcSample.SampleBase;

namespace RepoT.RavenDb.MvcSample.Controllers
{
    public class BookmarksController : BaseDocumentStoreController
    {
        private readonly BookmarkService _bookmarkService;
        private readonly BookmarkRepository _bookmarkRepository;

        public BookmarksController()
        {
            _bookmarkRepository = new BookmarkRepository(new RavenSessionFactory());
            _bookmarkService = new BookmarkService(_bookmarkRepository, new UnitOfWork(new RavenSessionFactory()));
        }

        public ViewResult Index()
        {
            var model = _bookmarkService.GetAll().OrderByDescending(item => item.DateCreated);
            return View(model);
        }

        public ViewResult Details(string id)
        {
            var model = DocumentSession.Load<Bookmark>(id);
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
            bool add = _bookmarkService.Add(bookmark, CommitMode.Repository);
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
            _bookmarkService.Add(bookmark, CommitMode.Repository);
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
            var model = new BookmarksByTagViewModel { Tag = tag };
            model.Bookmarks =
                _bookmarkService.GetMany(item => item.Tags.Any(t => t == tag)).OrderByDescending(i => i.DateCreated).
                    ToList();
            return View(model);
        }
    }
}