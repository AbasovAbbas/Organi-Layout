using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organi.Domain.DataContext;
using Organi.Domain.Entity;

namespace OrganiWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppInfosController : Controller
    {
        private readonly OrganiDbContext _context;

        public AppInfosController(OrganiDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AppInfos
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppInfos.ToListAsync());
        }

        // GET: Admin/AppInfos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appInfo = await _context.AppInfos.FindAsync(id);
            if (appInfo == null)
            {
                return NotFound();
            }
            return View(appInfo);
        }

        // POST: Admin/AppInfos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppTitle,LogoPath,ImageLink,HashTag,Description,Phone,Address,Email,OpenTime,FacebookLink,InstagramLink,Githublink,TwitterLink,PinterestLink")] AppInfo appInfo)
        {
            if (id != appInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppInfoExists(appInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appInfo);
        }

        private bool AppInfoExists(int id)
        {
            return _context.AppInfos.Any(e => e.Id == id);
        }
    }
}
