using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;

namespace SocialBookmarking_FM.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly ApplicationDbContext db;
        public BookmarkController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Bookmarks.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Add categories ids to viewbag
            var categs = db.Categories.ToList();
            ViewBag.CategoryId = categs;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Bookmark b)
        {
            b.Date = DateTime.Now;
            b.rating = 0;
            db.Bookmarks.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
