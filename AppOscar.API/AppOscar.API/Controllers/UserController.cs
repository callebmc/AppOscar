using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppOscar.Models;
using AppOscar.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppOscar.API.Controllers
{
    [Route("usuarios")]
    [ApiController]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly AppOscarContext context;

        public UserController(AppOscarContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUsuarios(){
            List<User> usuarios;

            try
            {
                usuarios = await context.Usuarios.ToListAsync();
                if (usuarios == null)
                    return new NotFoundResult();
                return new OkObjectResult(usuarios);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}