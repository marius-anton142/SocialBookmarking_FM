using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using System.Net;

namespace SocialBookmarking_FM.Controllers
{
    public class Search : Controller
    {
        private readonly ApplicationDbContext db;
        public Search(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public IActionResult SearchBookmarks()
        {
            var orderBy = (string)Request.Form["orderBy"];
            var search = (string)Request.Form["search"];

            List<string> errors = new List<string>();

            var qbkm = (from x in db.Bookmarks
                        where x.Title == search
                        join y in db.Users
                        on x.UserId equals y.Id

                        select new { b = x, u = y });

            var qlbkm = (from x in db.Bookmarks
                         where x.Title != search
                         join y in db.Users
                         on x.UserId equals y.Id

                         select new { b = x, u = y });

            if (orderBy == "rating")
            {
                ViewBag.bkm = qbkm.OrderByDescending(c => c.b.rating).ToList();
                var lbkm = qlbkm.OrderByDescending(c => c.b.rating).ToList();
                var fbkm = lbkm.ToList();
                fbkm.Clear();
                foreach (var item in lbkm)
                {
                    if(Fastenshtein.Levenshtein.Distance(search, item.b.Title) < 3)
                    {
                        fbkm.Add(item);
                    }
                }
                ViewBag.lbkm = fbkm;
            } else
            {
                ViewBag.bkm = qbkm.OrderByDescending(c => c.b.Date).ToList();
                var lbkm = qlbkm.OrderByDescending(c => c.b.Date).ToList();
                var fbkm = lbkm.ToList();
                fbkm.Clear();
                foreach (var item in lbkm)
                {
                    if (Fastenshtein.Levenshtein.Distance(search, item.b.Title) < 3)
                    {
                        fbkm.Add(item);
                    }
                }
                ViewBag.lbkm = fbkm;

            }



            ViewBag.ccat = (from x in db.CollectionCategory select x).ToList();

            return View();
        }
    }
}
