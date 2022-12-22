using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SocialBookmarking_FM.Controllers
{
    public class CategoryCollectionController : Controller
    {

        private readonly ApplicationDbContext db;
        public CategoryCollectionController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(db.CollectionCategory.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            return View(db.CollectionCategory.Find(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Edit(CollectionCategory cc)
        {
            db.CollectionCategory.Update(cc);
            db.SaveChanges();

            Response.Redirect("/CategoryCollection/Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
            var categ = db.CollectionCategory.Find(id);
            db.CollectionCategory.Remove(categ);
            db.SaveChanges();

            Response.Redirect("/CategoryCollection/Index");
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Add()
        {
            var cName = (string)Request.Form["categoryId"];
            var bId = Int32.Parse(Request.Form["bookmarkId"]);

            List<string> errors = new List<string>();

            if (cName == "" || bId == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errors.Add("Invalid data");
                return Json(errors);
            }

            var existsCateg = db.CollectionCategory.Where(c => c.CategoryName == cName);

            int ccid;

            if (existsCateg.Count() == 0)
            {
                CollectionCategory newCC = new CollectionCategory();
                newCC.CategoryName = cName;
                
                db.CollectionCategory.Add(newCC);
                db.SaveChanges();

                ccid = newCC.Id;
            } else
            {
                ccid = existsCateg.FirstOrDefault().Id;
            }

            int cid;
            var existsCollection = db.Collection.Where(c => c.CategoryId == ccid && c.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (existsCollection.Count() == 0)
            {
                Collection newC = new Collection();
                newC.CategoryId = existsCateg.FirstOrDefault().Id;
                newC.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                db.Collection.Add(newC);
                db.SaveChanges();

                cid = newC.Id;
            } else
            {
                cid = existsCollection.FirstOrDefault().Id;
            }

            this.AddBookmarkCollection(bId, cid);

            var ok = new Dictionary<string, string>
            {
                ["Success"] = "Success"
            };
            return Ok(Json(ok));
        }

        public void AddBookmarkCollection(int bId, int cId)
        {
            var ebkm = db.Bookmarks.Find(bId);

            BookmarkCollection newCC = new BookmarkCollection();
            newCC.BookmarkId = bId;
            newCC.CollectionId = cId;

            db.BookmarkCollection.Add(newCC);
            db.SaveChanges();
        }
    }
}
