using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.bkm = (from x in db.Bookmarks
                           join y in db.Users
                           on x.UserId equals y.Id

                           join z in db.Categories
                           on x.CategoryId equals z.Id

                           select new { b = x, u = y, c = z }).ToList();
            return View();
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
        public void Create(Bookmark b)
        {
            b.Date = DateTime.Now;
            b.rating = 0;
            b.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            db.Bookmarks.Add(b);
            db.SaveChanges();
            Response.Redirect("/Bookmark/Index");
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            var bkm = (from x in db.Bookmarks
                       where x.Id == id
                       join y in db.Users
                       on x.UserId equals y.Id

                       join z in db.Categories
                       on x.CategoryId equals z.Id
                       
                       select new { b = x, u = y, c = z }).ToList()[0];
            ViewBag.bkm = bkm;

            ViewBag.currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var comments = (from x in db.Bookmarks
                            where x.Id == id

                            join z in db.Comments
                            on x.Id equals z.BookmarkId
                            
                            join y in db.Users
                            on z.UserId equals y.Id
                            
                            select new { b = x, u = y, c = z }).ToList();
            ViewBag.comments = comments;
            
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {
            var bkm = db.Bookmarks.Find(id);

            if (User.IsInRole("Admin") || bkm.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value) { 
                var categs = db.Categories.ToList();
                ViewBag.CategoryId = categs;
                return View(bkm);
            }
            return View("Index");
            
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Edit(Bookmark b)
        {
            if (User.IsInRole("Admin") || b.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value) {
                var b1 = db.Bookmarks.Find(b.Id);

                b.UserId = b1.UserId;
                b.Date = b1.Date;
                b.rating = b1.rating;
                
                db.Entry(b1).State = EntityState.Detached;

                
                db.Bookmarks.Update(b);
                db.SaveChanges();
            }
            
            Response.Redirect("/Bookmark/Edit/" + b.Id);

        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Delete(int id)
        {
            var b = db.Bookmarks.Find(id);

            if (User.IsInRole("Admin") || b.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                db.Bookmarks.Remove(b);
                db.SaveChanges();
                Response.Redirect("/Bookmark/Index");
            }
        }
    }
}
