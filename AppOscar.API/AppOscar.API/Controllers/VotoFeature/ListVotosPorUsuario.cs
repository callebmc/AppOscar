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
    public class ListVotosPorUsuario : IRequest<ListVotosResult>
    {
        public ListVotosPorUsuario(string idUsuario)
        {
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
        }

        public string IdUsuario { get; set; }
    }

    public class ListVotosPorUsuarioHandler : IRequestHandler<ListVotosPorUsuario, ListVotosResult>
    {
        private readonly AppOscarContext context;

        public ListVotosPorUsuarioHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListVotosResult> Handle(ListVotosPorUsuario request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ListVotosPorUsuarioInternalAsync(request.IdUsuario, cancellationToken);
        }

        private async Task<ListVotosResult> ListVotosPorUsuarioInternalAsync(string idUsuario, CancellationToken cancellationToken)
        {
            var votos = await context.Votos.Where(v => v.IdUsuario == idUsuario).ToListAsync(cancellationToken);
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
