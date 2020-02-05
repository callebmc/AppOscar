using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.Participacao
{
    /// <summary>
    /// Query para consultar todos os filmes que estão participando de uma categoria.
    /// </summary>
    public class ListFilmesPorCategoria : IRequest<ListFilmesPorCategoriaResult>
    {
        public Guid CategoriaId { get; set; }
    }

    public class ListFilmesPorCategoriaResult
    {
        public ListFilmesPorCategoriaResult(IEnumerable<Models.Participacao> participacaos)
        {
            Participacaos = participacaos ?? throw new ArgumentNullException(nameof(participacaos));
        }

        public IEnumerable<Models.Participacao> Participacaos { get; }
    }

    public class ListFilmesPorCategoriaHandler : IRequestHandler<ListFilmesPorCategoria, ListFilmesPorCategoriaResult>
    {
        private readonly ILogger<ListFilmesPorCategoriaHandler> logger;
        private readonly AppOscarContext context;

        public ListFilmesPorCategoriaHandler(ILogger<ListFilmesPorCategoriaHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListFilmesPorCategoriaResult> Handle(ListFilmesPorCategoria request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListFilmesPorCategoriaInternalAsync(request.CategoriaId, cancellationToken);
        }

        private async Task<ListFilmesPorCategoriaResult> ListFilmesPorCategoriaInternalAsync(Guid categoriaId, CancellationToken cancellationToken)
        {
            // Buscando a categoria com suas participações e filmes!
            var categoria = await context.Categorias
                .AsNoTracking()     // Previnindo que o EFCore faça track da nossa query e seus objetos
                .Include(c => c.Participantes)
                    .ThenInclude(p => p.Filme)
                .SingleOrDefaultAsync(c => c.IdCategoria == categoriaId, cancellationToken);

            if (categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            var participacaos = categoria.Participantes
                //.Select(p => p.Id)   // Pegando os filmes para cada Participacao
                .Select(f => new Models.Participacao  // Garantindo que estamos pegando apenas os campos relevantes para essa query
                {
                    Id = f.Id,
                    IdCategoria = f.IdCategoria,
                    IdFilme = f.IdFilme,
                    Filme = new Filme
                    {
                        NomeFilme = f.Filme.NomeFilme,
                        FilmePhotoUrl = f.Filme.FilmePhotoUrl
                    }
                });

            return new ListFilmesPorCategoriaResult(participacaos);
        }
    }
}
