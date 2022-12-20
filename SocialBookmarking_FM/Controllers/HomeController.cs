using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Diagnostics;

namespace SocialBookmarking_FM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        private readonly ApplicationDbContext db;
        

        public IActionResult Index()
        {
            var bkm = (from x in db.Bookmarks 
                       join y in db.Users
                       on x.UserId equals y.Id
                       
                       select new {b = x, u = y}).OrderByDescending(x => x.b.Date).ToList();
            ViewBag.bkm = bkm;
            return View();
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