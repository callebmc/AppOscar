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
    public class ListParticipacaoPorCategoria : IRequest<ListParticipacaoResult>
    {
        public Guid IdCategoria { get; set; }
    }

    public class ListParticipacaoResult
    {
        public DateTime DthCriacao { get; set; }
    }

    public class ListParticipacaoPorCategoriaHandler : IRequestHandler<ListParticipacaoPorCategoria, ListParticipacaoResult>
    {
        private readonly ILogger<ListParticipacaoPorCategoriaHandler> logger;
        private readonly AppOscarContext context;

        public ListParticipacaoPorCategoriaHandler(ILogger<ListParticipacaoPorCategoriaHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListParticipacaoResult> Handle(ListParticipacaoPorCategoria request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ListParticipacaoPorCategoriaInternalAsync(request.IdCategoria, cancellationToken);
        }

        private async Task<ListParticipacaoResult> ListParticipacaoPorCategoriaInternalAsync(Guid idCategoria, CancellationToken cancellationToken)
        {
            var categoria = await context.Participacoes.Include(c => c.Categoria).Where(c => c.IdCategoria == idCategoria).Include(f => f.Filme).ToListAsync();
            if(categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            return new ListParticipacaoResult { DthCriacao = DateTime.Now};
        }
    }
}
