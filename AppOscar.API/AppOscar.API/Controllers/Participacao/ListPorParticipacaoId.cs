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
    // IEnumerable<Models.Participacao>
    public class ListPorParticipacaoId : IRequest<ListPorParticipacaoIdResult>
    {
        public int Id { get; set; }
    }

    public class ListPorParticipacaoIdResult
    {
        public ListPorParticipacaoIdResult(Models.Participacao participacao)
        {
            Participacao = participacao;
        }

        public Models.Participacao Participacao { get; }
    }

    public class ListPorParticipacaoIdHandler : IRequestHandler<ListPorParticipacaoId, ListPorParticipacaoIdResult>
    {
        private readonly ILogger<ListFilmesPorCategoriaHandler> logger;
        private readonly AppOscarContext context;

        public ListPorParticipacaoIdHandler(ILogger<ListFilmesPorCategoriaHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListPorParticipacaoIdResult> Handle(ListPorParticipacaoId request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListFilmesPorIdInternalAsync(request.Id, cancellationToken);
        }

        private async Task<ListPorParticipacaoIdResult> ListFilmesPorIdInternalAsync(int id, CancellationToken cancellationToken)
        {
            Guid teste = Guid.NewGuid();
            // Buscando a categoria com suas participações e filmes!
            var categoria = await context.Categorias
                .AsNoTracking()     // Previnindo que o EFCore faça track da nossa query e seus objetos
                .Include(c => c.Participantes)
                    .ThenInclude(p => p.Filme)
                .SingleOrDefaultAsync(c => c.IdCategoria == teste, cancellationToken);

            var participacao = await context.Participacoes
                .AsNoTracking()
                .Include(c => c.Categoria)
                .Include(c => c.Filme)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (participacao is null)
                throw new KeyNotFoundException("Categoria não encontrada");


            //var participacaos = participacao.
            //    .Select(f => new Models.Participacao
            //    {
            //        Id = f.Id,
            //        IdCategoria = f.IdCategoria,
            //        IdFilme = f.IdFilme,
            //        Filme = new Filme
            //        {
            //            NomeFilme = f.Filme.NomeFilme,
            //            FilmePhotoUrl = f.Filme.FilmePhotoUrl
            //        },
            //        Categoria = new Categoria
            //        {
            //            NomeCategoria = f.Categoria.NomeCategoria
            //        }
            //    });

            return new ListPorParticipacaoIdResult(participacao);
        }
    }
}
