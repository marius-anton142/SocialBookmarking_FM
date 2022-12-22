using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using System.Data;
using System.Security.Claims;

namespace SocialBookmarking_FM.Controllers
{
    public class BookmarkCollectionController : Controller
    {
        private readonly ApplicationDbContext db;
        public BookmarkCollectionController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]

        public void Delete(int id)
        {
            var bc = db.BookmarkCollection.Find(id);
            var isUser = db.Collection.Where(c => c.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value && c.Id == bc.CollectionId).Count() > 0;

            if (User.IsInRole("Admin") || isUser )
            {
                db.BookmarkCollection.Remove(bc);
                db.SaveChanges();
                Response.Redirect("/Bookmark/Index");
            };

        }
    }
}
