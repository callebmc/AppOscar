using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.API.ViewModels.Categoria;
using AppOscar.API.ViewModels.Filme;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoriaList>))]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _categoriaRepository.GetAllCategorias();

            var catResult = new List<CategoriaList>();
            foreach (var categoria in categorias)
                catResult.Add(new CategoriaList
                {
                    Id = categoria.IdCategoria,
                    NomeCategoria = categoria.NomeCategoria,
                    PontosCategoria = categoria.PontosCategoria
                });

            return Ok(catResult);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaCreate novaCategoria)
        {
            CategoriaCreateCommand categoriaCreate = new CategoriaCreateCommand(novaCategoria.NomeCategoria, novaCategoria.PontosCategoria);

            var response = await _mediator.Send(categoriaCreate);
            return Ok(response);
        }
    }
}