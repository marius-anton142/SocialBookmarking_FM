using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Data;

namespace SocialBookmarking_FM.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var categ = db.Categories.Find(id);
            return View(categ);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Category c)
        {
            db.Categories.Update(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var categ = db.Categories.Find(id);
            db.Categories.Remove(categ);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Show(int id)
        {
            var categ = db.Categories.Find(id);
            return View(categ);
        }
    }
}
