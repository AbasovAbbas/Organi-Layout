using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;
using Organi.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly OrganiDbContext db;
        public HomeController(OrganiDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Categories = db.Categories.Where(c => c.IsFeatured == true).ToList();
            model.FeateredProducts = db.Products
                .Include(p => p.Category)//lazy loading
                .Include(p => p.Images)
               .Where(p => p.Category.IsFeatured == true).ToList();

            //evvelce model ve viewbagdan istifade etdik, sonra viewmodelden

            //var categories = db.Categories.Where(x => x.IsFeatured == true).ToList();
            //ViewBag.Products = db.Products.Include(p => p.Category)//lazy loading
            //    .Where(p => p.Category.IsFeatured == true).ToList();  
            return View(model);
        }

        public  IActionResult Contact()
        {
            ViewBag.AppInfo = db.AppInfos.FirstOrDefault();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact contact)
        {
            //tekrar yazmaq lazimdi, cunki viewbag bir request boyu aktiv olur
            ViewBag.AppInfo = db.AppInfos.FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "Sorgunuz qebul olundu";
                return View();
            }
            return View(contact);
        }

        public IActionResult Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(new
                {
                    error = true,
                    message = "Email doldurulmayib"
                });
            }
            else if (db.Subcribes.Any(s => s.Email == email)){
                return Json(new
                {
                    error = true,
                    message = "Artiq abune olmusunuz"
                });
            }
            else
            {
                var subcribe = new Subscribe
                {
                    Email = email
                };
                db.Subcribes.Add(subcribe);
                db.SaveChanges();

                return Json(new
                {
                    error = false,
                    message = "Abune oldunuz"
                });
            }
        }
    }
}
