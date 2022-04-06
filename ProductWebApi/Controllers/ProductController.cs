using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using ProductWebApi.Models.Dso;
using ProductWebApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public ProductController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result =await _context.Products.ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductAddDso model)
        {
            if (string.IsNullOrWhiteSpace(model.ImageToken))
            {
                return BadRequest();
            }   
            Image image = await _context.Images.FirstOrDefaultAsync(x => x.Token.Equals(model.ImageToken));
            if (image == null)
            {
                return BadRequest();
            }
            Product product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Unit = model.Unit,
                ImageId = image.Id
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(); 
            
        }
    }
}
