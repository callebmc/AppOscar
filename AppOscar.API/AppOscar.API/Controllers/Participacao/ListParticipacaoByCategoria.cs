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
    public class ListParticipacaoByCategoria : IRequest<ListParticipacaoByCategoriaResult>
    {
            public Guid IdCategoria { get; set; }
    }

    public class ListParticipacaoByCategoriaResult
    {
        public IEnumerable<Models.Participacao> participacaos;

        public ListParticipacaoByCategoriaResult(IEnumerable<Models.Participacao> participacaos)
        {
            this.participacaos = participacaos ?? throw new ArgumentNullException(nameof(participacaos));
        }
    }

    public class ListParticipacaoByCategoriaHandler : IRequestHandler<ListParticipacaoByCategoria, ListParticipacaoByCategoriaResult> {
        private readonly ILogger<ListCategoriaPorFilmeHandler> logger;
        private readonly AppOscarContext context;

        public ListParticipacaoByCategoriaHandler(ILogger<ListCategoriaPorFilmeHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListParticipacaoByCategoriaResult> Handle(ListParticipacaoByCategoria request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListParticipacaoPorCategoriaInternalAsyc(request.IdCategoria, cancellationToken);
        }

        private async Task<ListParticipacaoByCategoriaResult> ListParticipacaoPorCategoriaInternalAsyc(Guid idCategoria, CancellationToken cancellationToken)
        {
            var participacao = await context.Participacoes
                .Include(c => c.Categoria)
                .Where(f => f.IdCategoria == idCategoria)
                .Include(c => c.IdFilme == c.)
                .ToListAsync();

            return new ListCategoriasPorFilmeResult(participacao);
        }
    }
}
