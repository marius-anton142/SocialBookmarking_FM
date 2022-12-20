using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;
using System.Security.Claims;

namespace SocialBookmarking_FM.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext db;
        public CommentController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public void Create(int id)
        {
            Comment c = new Comment();
            //var x = Request.Form["bookmarkId"];
            c.BookmarkId = id;
            c.Content = Request.Form["content"];

            //c.BookmarkId = Int32.Parse(HttpContext.Request.Query["bookmarkId"].ToString());
            c.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            c.Date = DateTime.Now;

            db.Comments.Add(c);
            db.SaveChanges();

            // Redirect to returnurl
            Response.Redirect("/Bookmark/Show/" + id);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Delete(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var c = db.Comments.Find(id);
                db.Comments.Remove(c);
                db.SaveChanges();
                Response.Redirect("/Bookmark/Show/" + c.BookmarkId);
            }
            else
            {
                var c = db.Comments.Find(id);
                if (c.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    db.Comments.Remove(c);
                    db.SaveChanges();
                    Response.Redirect("/Bookmark/Show/" + c.BookmarkId);
                }
            }

           
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Edit(int id)
        {
            {
                var c = db.Comments.Find(id);
                if (User.IsInRole("Admin") || c.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    return View(c);
                }

                return View("/Bookmark/Show/" + c.BookmarkId);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public void Edit(Comment c)
        {
            {
                var c1 = db.Comments.Find(c.CommentId);

                c.BookmarkId = c1.BookmarkId;
                c.UserId = c1.UserId;
                c.Date = c1.Date;

                db.Entry(c1).State = EntityState.Detached;

                db.Comments.Update(c);
                db.SaveChanges();

                Response.Redirect("/Bookmark/Show/" + c.BookmarkId);
            }
        }
    }
}
