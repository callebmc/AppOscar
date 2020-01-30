﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("{id}", Name="ListPorCategoria")]
        public async Task<IActionResult> ListPorCategoria (string idRota)
        {
            if (idRota is null)
                return BadRequest();

            ListParticipacaoPorCategoria teste = new ListParticipacaoPorCategoria() { IdCategoria = Guid.Parse(idRota) };

            var result = await mediator.Send(teste);

            return new OkObjectResult(result);
        }
    }
}