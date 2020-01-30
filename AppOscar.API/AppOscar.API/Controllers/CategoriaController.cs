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

namespace AppOscar.API.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppOscarContext context;

        public CategoriaController(AppOscarContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Categoria>> GetCategorias()
        {
            List<Categoria> categorias;

            try
            {
                categorias = await context.Categorias.ToListAsync();
                if (categorias == null)
                    return new NotFoundResult();
                return new OkObjectResult(categorias);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria novaCategoria)
        {
            Categoria categoria;

            try
            {
                categoria = new Categoria()
                {
                    NomeCategoria = novaCategoria.NomeCategoria,
                    PontosCategoria = novaCategoria.PontosCategoria
                };

                context.Categorias.Add(categoria);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}