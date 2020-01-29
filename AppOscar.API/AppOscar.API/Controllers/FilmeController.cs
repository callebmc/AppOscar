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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class FilmeController : ControllerBase
    {

        private readonly AppOscarContext context;

        public FilmeController(AppOscarContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Filme>> GetFilmes()
        {
            List<Filme> filmes;

            try
            {
                filmes = await context.Filmes.ToListAsync();
                if (filmes == null)
                    return new NotFoundResult();
                return new OkObjectResult(filmes);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Filme>> CreateCategoria([FromBody] Filme novoFilme)
        {
            Filme filme;

            try
            {
                filme = new Filme()
                {
                    NomeFilme = novoFilme.NomeFilme,
                };

                context.Filmes.Add(filme);
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