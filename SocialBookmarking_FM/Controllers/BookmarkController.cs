using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Security.Claims;

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
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            var categs = db.Categories.ToList();
            ViewBag.CategoryId = categs;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create(Bookmark b)
        {
            b.Date = DateTime.Now;
            b.rating = 0;
            b.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            db.Bookmarks.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
