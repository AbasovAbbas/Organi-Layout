using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organi.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI.appCode.Components
{
    public class ProductByCategoryViewComponent : ViewComponent
    {
        readonly OrganiDbContext db;
        public ProductByCategoryViewComponent(OrganiDbContext db)
        {
            this.db = db;   
        }

        public IViewComponentResult Invoke(string title)
        {
            //var query = db.Products.AsQueryable();
            var products = db.Products
                .Include(p => p.Images)
                .OrderByDescending(x => x.Id).Take(3).ToList();//--muxtelif sertlere gore bir nece query yazmaga imkan verir ve yekunda eyni productsi gondermek olur/
              //-- bu sekilde model gonderib view terfden qarsilamaq da mumkundur-- 
            ViewBag.CategoryTitle = title;
            return View(products);
        }
    }
}
