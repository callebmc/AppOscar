using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.VotoFeature
{
    public class ListVotos : IRequest<ListVotosResult>
    {
    }

    public class ListVotosHandler : IRequestHandler<ListVotos, ListVotosResult>
    {
        private readonly AppOscarContext context;

        public ListVotosHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListVotosResult> Handle(ListVotos request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));


            return ListVotosInternalAsync(cancellationToken);
        }

        private async Task<ListVotosResult> ListVotosInternalAsync(CancellationToken cancellationToken)
        {
            var votos = await context.Votos.ToListAsync(cancellationToken);
            return new ListVotosResult(votos.Select(v => new Voto { Id = v.Id, DthCriacao = v.DthCriacao, IdParticipacao = v.IdParticipacao, IdUsuario = v.IdUsuario }));
        }
    }
}
