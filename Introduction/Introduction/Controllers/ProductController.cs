using Introduction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Introduction.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Keyboard", Description = "Mechanical keyboard", Price = 100 },
            new Product { Id = 2, Name = "Mouse", Description = "Gaming mouse", Price = 50 },
            new Product { Id = 3, Name = "Monitor", Description = "4K monitor", Price = 500 }
        };

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = Products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
