using System.Threading.Tasks;
using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.API.ViewModels.Categoria;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _categoriaRepository.GetAllCategorias();

            return Ok(categorias);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategoria([FromBody]CategoriaCreate novaCategoria)
        {
            CategoriaCreateCommand categoriaCreate = new CategoriaCreateCommand() { Id = novaCategoria.Id, NomeCategoria = novaCategoria.NomeCategoria, PontosCategoria = novaCategoria.PontosCategoria };

            var response = await _mediator.Send(categoriaCreate);
            return Ok(response);
        }
    }
}