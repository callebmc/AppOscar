using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.VotoFeature
{
    public class CreateVoto : IRequest<CreateVotoResult>
    {
        [Required]
        public int IdParticipacao { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string IdUsuario { get; set; }
    }

    public class CreateVotoResult
    {
        public CreateVotoResult(DateTimeOffset dthCriacao, int idVoto)
        {
            DthCriacao = dthCriacao;
            IdVoto = idVoto;
        }

        public DateTimeOffset DthCriacao { get; }

        public int IdVoto { get; }
    }

    public class CreateVotoHandler : IRequestHandler<CreateVoto, CreateVotoResult>
    {
        private readonly AppOscarContext context;

        public CreateVotoHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CreateVotoResult> Handle(CreateVoto request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateVotoInternalAsync(request.IdParticipacao, request.IdUsuario, cancellationToken);
        }

        private async Task<CreateVotoResult> CreateVotoInternalAsync(int idParticipacao, string idUsuario, CancellationToken cancellationToken)
        {
            // Vendo se a participacao existe
            var participacao = await context.Participacoes
                .Include(p => p.Votos)
                .Include(p => p.Categoria)
                    .ThenInclude(c => c.Participantes)
                        .ThenInclude(p => p.Votos)
                .SingleOrDefaultAsync(p => p.Id == idParticipacao, cancellationToken);

            if (participacao is null)
                throw new KeyNotFoundException("Participacao não encontrada");

            // Vendo se já existe algum voto para este usuário
            if (participacao.Votos.Any(v => v.IdUsuario == idUsuario))
                throw new InvalidOperationException("O usuário já votou neste filme e categoria");

            // Vendo se o usuário já votou nesta Categoria para qualquer filme
            if (participacao.Categoria.Participantes.Any(p => p.Votos.Any(v => v.IdUsuario == idUsuario)))
                throw new InvalidOperationException("O usuário já votou nesta categoria em outro filme");

            // Tudo OK, criar o voto
            var novoVoto = new Voto
            {
                DthCriacao = DateTimeOffset.Now,
                IdUsuario = idUsuario,
                IdParticipacao = participacao.Id
            };

            participacao.Votos.Add(novoVoto);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateVotoResult(novoVoto.DthCriacao, novoVoto.Id);
        }
    }
}
