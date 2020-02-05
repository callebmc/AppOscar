using AppOscar.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.Participacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipacaoController : ControllerBase
    {
        private readonly IMediator mediator;

        public ParticipacaoController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew([FromBody] CreateParticipacao request)
        {
            if (request is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await mediator.Send(request);

            return Ok(result.IdParticipacao);
        }

        [HttpGet("by-categoria/{id}", Name = "ListPorCategoria")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Filme>))]  // TODO: O Swagger se perde aqui por causa do ciclico, aparentemente é um bug do SwaggerUi (Javascript, nem o do DOTNET) e esta sendo investigado!
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListPorCategoria([FromRoute] Guid id)
        {
            ListFilmesPorCategoria request = new ListFilmesPorCategoria { CategoriaId = id };

            try
            {
                var result = await mediator.Send(request);

                return new OkObjectResult(result.Participacaos);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("by-filme/{id}", Name = "ListPorFilme")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Filme>))]  // TODO: O Swagger se perde aqui por causa do ciclico, aparentemente é um bug do SwaggerUi (Javascript, nem o do DOTNET) e esta sendo investigado!
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListPorFilme([FromRoute] Guid id)
        {
            ListCategoriasPorFilme request = new ListCategoriasPorFilme { FilmeId = id };

            try 
            {
                var result = await mediator.Send(request);
                return new OkObjectResult(result.Categorias);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
