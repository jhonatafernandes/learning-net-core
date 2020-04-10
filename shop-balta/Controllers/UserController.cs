using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using shop_balta.Services;
using shop_balta.Models;

namespace shop_balta.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> Post(
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
                return BadRequest(new { message = "Não foi possível criar o usuário" });

            }
        }


            [HttpPost]
            [Route("login")]
            public async Task<ActionResult<dynamic>> Authenticate(
                [FromServices] DataContext context,
                [FromBody] User model)
            {

            var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };

        }

    }
}
