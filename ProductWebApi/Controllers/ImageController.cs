using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;
using ProductWebApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public IHostingEnvironment _enviroment;
        public ShopDbContext _context;
        public ImageController(IHostingEnvironment enviroment, ShopDbContext context)
        {
            _enviroment = enviroment;
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> AddImage(IFormFile file)
        {
            string key = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10)}";
            string ext = Path.GetExtension(file.FileName);

            string path = Path.Combine(_enviroment.ContentRootPath,"uploads","images", $"{key}{ext}");
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fs);
            }
            //string name = _enviroment.ContentRootPath;

            _context.Add(new Image
            {
                Token = key,
                FileName = Path.Combine("uploads", "images", $"{key}{ext}")
            });
            await _context.SaveChangesAsync();
            return Ok(new
            {
                error = false,
                token = key
            });
        }
    }
}
