using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;
using Organi.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrganiWebUI.Controllers
{
    public class ShopController : Controller
    {
        readonly OrganiDbContext db; 
        public ShopController(OrganiDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var products = db.Products
                .Include(p => p.Images).ToList();
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var model = new ProductDetailsViewModel();
            var product = db.Products
                .Include(p => p.Images) 
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            model.Currrent = product;

            model.RelatedProducts = db.Products
                .Include(p =>p.Images)
                .Where(p => p.CategoryId == product.CategoryId && p.Id!=product.Id).ToList();
            return View(model);
        }

        public IActionResult ShoppingCart()
        {
            string[] ids = Request.Cookies["card-item"]?.Split(','); 

            if (ids !=null && ids.Length>0)
            {
                int[] ItemIds = ids.Where(i => Regex.IsMatch(i, @"\d+")).Select(i => int.Parse(i)).ToArray();
                var data = db.Products
                    .Include(p =>p.Images)
                    .Where(p => ItemIds.Contains(p.Id)).ToList();
                return View(data);
            }
            return View(Enumerable.Empty<Product>());
        }
    }
}
