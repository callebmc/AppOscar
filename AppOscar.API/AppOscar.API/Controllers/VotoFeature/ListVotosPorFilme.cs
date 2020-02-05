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
    public class ListVotosPorFilme : IRequest<ListVotosResult>
    {
        public ListVotosPorFilme(Guid filmeId)
        {
            FilmeId = filmeId;
        }

        public Guid FilmeId { get; set; }
    }

    public class ListVotosPorFilmeHandler : IRequestHandler<ListVotosPorFilme, ListVotosResult>
    {
        private readonly AppOscarContext context;

        public ListVotosPorFilmeHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListVotosResult> Handle(ListVotosPorFilme request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ListVotosPorFilmeInternalAsync(request.FilmeId, cancellationToken);
        }

        private async Task<ListVotosResult> ListVotosPorFilmeInternalAsync(Guid filmeId, CancellationToken cancellationToken)
        {
            var filme = await context.Filmes
                .Include(f => f.Participantes)
                    .ThenInclude(p => p.Votos)
                .SingleOrDefaultAsync(cancellationToken);

            if (filme is null)
            {
                throw new KeyNotFoundException("O filme solicitado não existe");
            }

            var votos = filme.Participantes.SelectMany(p => p.Votos);
            return new ListVotosResult(votos.Select(v => new Voto
            {
                Id = v.Id,
                DthCriacao = v.DthCriacao,
                IdParticipacao = v.IdParticipacao,
                IdUsuario = v.IdUsuario
            }));

        }
    }
}
