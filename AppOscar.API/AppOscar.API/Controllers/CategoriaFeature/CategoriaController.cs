using AppOscar.API.Controllers.CategoriaFeature;
using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.API.ViewModels.Categoria;
using AppOscar.API.ViewModels.Filme;
using AppOscar.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.Parametros
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(IMediator mediator, ICategoriaRepository categoriaRepository)
        {
            _mediator = mediator;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Categoria>))]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _mediator.Send(new ListAllCategorias());

            return Ok(categorias);

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CreateCategoria request)
        {
            if (request is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request);


            return Ok(result.IdCategoria);
        }
    }
}