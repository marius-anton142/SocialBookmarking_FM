using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Security.Claims;

namespace SocialBookmarking_FM.Controllers
{
    public class VoteController : Controller
    {
        private readonly ApplicationDbContext db;
        public VoteController(ApplicationDbContext context)
        {
            db = context;
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Toggle(int id)
        {
            Vote v = new Vote();
            v.BookmarkId = id;
            v.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var exists = (from x in db.Votes
                          where x.UserId == v.UserId && x.BookmarkId == v.BookmarkId
                          select new {a = 1}).ToList().Count() > 0;

            if (exists)
            {
                db.Votes.Remove(v);
                db.SaveChanges();

                var bkm = db.Bookmarks.Find(v.BookmarkId);
                --bkm.rating;
                db.Bookmarks.Update(bkm);
                db.SaveChanges();

                Response.Redirect("/Bookmark/Show/" + v.BookmarkId);
            }
            else
            {

                db.Votes.Add(v);
                db.SaveChanges();

                var bkm = db.Bookmarks.Find(v.BookmarkId);
                ++bkm.rating;
                db.Bookmarks.Update(bkm);
                db.SaveChanges();

                Response.Redirect("/Bookmark/Show/" + v.BookmarkId);
            }

        }
    }
}
