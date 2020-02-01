using AppOscar.API.Controllers.FilmeFeature;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilmeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFilmes()
        {

            var filmes = await _mediator.Send(new ListAllFilme());

            return Ok(filmes.Filmes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilme([FromBody]CreateFilme request)
        {
            if (request is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request);

            return Ok(result.IdFilme);
        }
    }
}