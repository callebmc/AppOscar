﻿using MediatR;
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
        public async Task<IActionResult> ListVotos([FromQuery] Guid? categoria, [FromQuery] Guid? filme, [FromQuery] string usuario)
        {
            var hasCategoria = categoria.HasValue;
            var hasFilme = filme.HasValue;
            var hasUsuario = !string.IsNullOrWhiteSpace(usuario);

            if (hasCategoria)
            {
                try
                {
                    var result = await mediator.Send(new ListVotosPorCategoria(categoria.Value));
                    return Ok(result.Votos);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }

            }
            else if (hasFilme)
            {
                try
                {
                    var result = await mediator.Send(new ListVotosPorFilme(filme.Value));
                    return Ok(result.Votos);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
            }
            else if (hasUsuario)
            {
                var result = await mediator.Send(new ListVotosPorUsuario(usuario));
                return Ok(result.Votos);
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

        [HttpGet("checkVoto/{usuario}", Name = "ChecarVoto")]
        public async Task<IActionResult> CheckVoto([FromRoute] string usuario)
        {
            try
            {
                var result = await mediator.Send(new CheckVoto(usuario));

                return Ok(result.JaVotou);
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
