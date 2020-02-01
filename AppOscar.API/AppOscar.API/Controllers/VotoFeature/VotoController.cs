using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.VotoFeature
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly IMediator mediator;

        public VotoController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> ListVotos([FromQuery] string categoria, [FromQuery] string filme, [FromQuery] string usuario)
        {
            var hasCategoria = !string.IsNullOrWhiteSpace(categoria);
            var hasFilme = !string.IsNullOrWhiteSpace(filme);
            var hasUsuario = !string.IsNullOrWhiteSpace(usuario);

            if (hasCategoria)
            {
                throw new NotImplementedException();
            }
            else if (hasFilme)
            {
                throw new NotImplementedException();
            }
            else if (hasUsuario)
            {
                throw new NotImplementedException();
            }
            else
            {
                var result = await mediator.Send(new ListVotos());
                return Ok(result.Votos);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoto([FromBody] CreateVoto votoCommand)
        {
            try
            {
                var result = await mediator.Send(votoCommand);
                return Ok(result.DthCriacao);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
