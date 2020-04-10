using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_balta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shop_balta.Controllers
{
   
    
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get(
            [FromServices] DataContext context)
        {
            try
            {
                var categories = await context.User.AsNoTracking().ToListAsync();
                return Ok(categories);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar as categorias" });
            }
            
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var category = await context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada" });

                return Ok(category);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar a categoria" });
            }
            
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromBody] Category model,
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.User.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria." });

            }
           
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(
            int id, 
            [FromBody]Category model,
            [FromServices] DataContext context)
        {


            if(id != model.Id) 
            {
                return NotFound(new { message = "Categoria não encontrada!" });
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar a categoria." });
            }



        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var category = await context.User.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.User.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new { message = "Categoria deletada com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir a categoria." });
            }
            
        }

       
    }
}
