using Microsoft.AspNetCore.Mvc;
using Organi.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebUI.appCode.Components
{
    public class SubscribeViewComponent : ViewComponent
    {
        readonly OrganiDbContext db;
        public SubscribeViewComponent(OrganiDbContext db)
        {
            this.db = db;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
