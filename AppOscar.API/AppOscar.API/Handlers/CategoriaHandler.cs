using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Handlers
{
    public class CategoriaHandler : IRequestHandler<CategoriaCreateCommand, string>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<string> Handle(CategoriaCreateCommand request, CancellationToken ct)
        {
            var categoria = new Categoria { NomeCategoria = request.NomeCategoria, PontosCategoria = request.PontosCategoria, IdCategoria = Guid.NewGuid() };
            await _categoriaRepository.Save(categoria);

            return await Task.FromResult("Categoria Cadastrada");
        }
    }
}
