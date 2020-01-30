using AppOscar.API.Domain;
using AppOscar.API.Repositories;
using AppOscar.API.ViewModels.Filme;
using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Handlers
{
    public class FilmesHandler : IRequestHandler<FilmeCreateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IFilmeRepository _filmeRepository;

        public FilmesHandler(IMediator mediator, IFilmeRepository filmeRepository)
        {
            _mediator = mediator;
            _filmeRepository = filmeRepository;
        }

        public async Task<string> Handle(FilmeCreateCommand request, CancellationToken ct)
        {
            var filme = new Filme { IdFilme = request.IdFilme, NomeFilme = request.NomeFilme };
            await _filmeRepository.Save(filme);

            return await Task.FromResult("Filme Cadastrado");
        }
    }
}
