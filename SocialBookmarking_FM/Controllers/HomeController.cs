using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Diagnostics;

namespace SocialBookmarking_FM.Controllers
{
    public class HomeController : Controller
    {
        private const int PAGINATION = 2;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        private readonly ApplicationDbContext db;
        

        public IActionResult Index(string? order, int? start)
        {
            order = order ?? "date";
            start = start ?? 0;

            ViewBag.ccat =(from x in db.CollectionCategory select x).ToList();

            ViewBag.prevOrder = order;

            if ((string)order == "date")
            {
                var qbkm = (from x in db.Bookmarks
                            join y in db.Users
                            on x.UserId equals y.Id

                            select new { b = x, u = y }).OrderByDescending(x => x.b.Date);
                if (qbkm.Count() - start - 1 > 0)
                {
                    ViewBag.next = start + PAGINATION ;
                } else
                {
                    ViewBag.next = -1;
                }

                if (start > 0)
                {
                    ViewBag.prev = start - PAGINATION;
                }
                else
                {
                    ViewBag.prev = -1;
                }

                var bkm = qbkm.Skip((int)start).Take(PAGINATION).ToList();

                ViewBag.bkm = bkm;
                ViewBag.order = "rating";
                return View();
            } else
            {
                var qbkm = (from x in db.Bookmarks
                           join y in db.Users
                           on x.UserId equals y.Id

                           select new { b = x, u = y }).OrderByDescending(x => x.b.rating);

                if (qbkm.Count() - start - 1 > 0)
                {
                    ViewBag.next = start + PAGINATION;
                } else
                {
                    ViewBag.next = -1;
                }

                if (start > 0)
                {
                    ViewBag.prev = start - PAGINATION;
                }
                else
                {
                    ViewBag.prev = -1;
                }

                var bkm = qbkm.Skip((int)start).Take(PAGINATION).ToList();
                ViewBag.bkm = bkm;
                ViewBag.order = "date";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}