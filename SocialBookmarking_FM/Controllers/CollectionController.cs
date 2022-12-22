using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using System.Security.Claims;

namespace SocialBookmarking_FM.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext db;
        public CollectionController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult ViewCollection()
        {
            ViewBag.collection = (
                from x in db.Collection
                where x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value
                join y in db.BookmarkCollection
                on x.Id equals y.CollectionId
                join yy in db.Bookmarks
                on y.BookmarkId equals yy.Id
                join z in db.CollectionCategory
                on x.CategoryId equals z.Id
                select new {col = x, bok = yy, cat = z, bcid = y.Id}
                ).OrderBy(c => c.col.Id).ToList();
            return View();
        }
    }
}
