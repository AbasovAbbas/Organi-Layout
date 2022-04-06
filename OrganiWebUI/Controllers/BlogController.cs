using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI.Controllers
{
    public class BlogController : Controller
    {
        readonly OrganiDbContext db;
        public BlogController(OrganiDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var posts = db.Posts
                .Include(p =>p.Category)
                .OrderByDescending(p => p.Id).ToList();
            return View(posts);
        }
        [Route("{lang}-post/{id}/{title}")]
        public IActionResult Details(int id)
        {
            var post = db.Posts
                .Include(p =>p.Category)
                .Include(p =>p.Comments)
                .FirstOrDefault(p =>p.Id == id);
            return View(post);
        }
        public IActionResult Comment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Json(new
                {
                    error = false,
                    message = comment.Message,
                    commentCount = db.Comments.Count(c => c.PostId ==comment.PostId)
                });
            }
            return Json(new
            {
                error = true,
                message = "Xeta bas verdi"
            });
        }
    }
}
