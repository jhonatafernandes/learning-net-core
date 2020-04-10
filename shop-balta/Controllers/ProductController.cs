using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_balta.Models;


namespace shop_balta.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Product>>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var products = await context.Products
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                return Ok(products);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os produtos" });
            }

        }


        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoriesId(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var products = await context.Products
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .Where(x => x.CategoryId == id)
                    .ToListAsync();

                return Ok(products);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os produtos" });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Post(
            [FromBody] Product model,
            [FromServices] DataContext context)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria." });

            }

        }
    }

   
}
