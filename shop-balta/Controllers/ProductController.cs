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
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get(
          [FromServices] DataContext context)
        {
            try
            {
                var products = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(products);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os produtos" });
            }

        }
    }

   
}
