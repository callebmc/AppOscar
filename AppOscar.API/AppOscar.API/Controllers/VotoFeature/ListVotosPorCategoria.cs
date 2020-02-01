using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.VotoFeature
{
    public class ListVotosPorCategoria : IRequest<ListVotosResult>
    {
        public ListVotosPorCategoria(Guid categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public Guid CategoriaId { get; set; }
    }

    public class ListVotosPorCategoriaHandler : IRequestHandler<ListVotosPorCategoria, ListVotosResult>
    {
        private readonly AppOscarContext context;

        public ListVotosPorCategoriaHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<ListVotosResult> Handle(ListVotosPorCategoria request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListVotosPorCategoriaInternalAsync(request.CategoriaId, cancellationToken);
        }

        private async Task<ListVotosResult> ListVotosPorCategoriaInternalAsync(Guid categoriaId, CancellationToken cancellationToken)
        {
            var categoria = await context.Categorias
                .Include(c => c.Participantes)
                    .ThenInclude(p => p.Votos)
                .SingleOrDefaultAsync(c => c.IdCategoria == categoriaId);

            if (categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            var votos = categoria.Participantes.SelectMany(p => p.Votos);
            return new ListVotosResult(votos.Select(v => new Voto { Id = v.Id, DthCriacao = v.DthCriacao, IdParticipacao = v.IdParticipacao, IdUsuario = v.IdUsuario }));
        }
    }
}
