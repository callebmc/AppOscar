using System;
using System.Threading.Tasks;
using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.API.ViewModels.Filme;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppOscar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFilmeRepository _filmeRepository;

        public FilmeController(IMediator mediator, IFilmeRepository filmeRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _filmeRepository = filmeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFilmes()
        {

            var filmes = await _filmeRepository.GetAllFilmes();

            return Ok(filmes);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFilme([FromBody]FilmeCreate filme )
        {
            FilmeCreateCommand novoFilme = new FilmeCreateCommand(){ IdFilme = filme.IdFilme, NomeFilme = filme.NomeFilme };

            var response = await _mediator.Send(novoFilme);
            return Ok(response);
        }
    }
}