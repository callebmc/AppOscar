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

namespace AppOscar.API.Controllers.FilmeFeature
{
    public class ListAllFilme : IRequest<ListAllFilmeResult>
    {

    }

    public class ListAllFilmeResult
    {
        public ListAllFilmeResult(IEnumerable<Filme> filmes)
        {
            Filmes = filmes ?? throw new ArgumentNullException(nameof(filmes));
        }

        public IEnumerable<Filme> Filmes { get; }
    }

    public class ListAllFilmesHandler : IRequestHandler<ListAllFilme, ListAllFilmeResult>
    {
        private readonly ILogger<ListAllFilmesHandler> logger;
        private readonly AppOscarContext context;

        public ListAllFilmesHandler(ILogger<ListAllFilmesHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListAllFilmeResult> Handle(ListAllFilme request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListAllFilmeInternalAsync(cancellationToken);
        }

        private async Task<ListAllFilmeResult> ListAllFilmeInternalAsync(CancellationToken ct)
        {
            var filmes = await context.Filmes.ToListAsync(ct);

            var filmesResult = filmes
                .Select(c => new Filme
                {
                    IdFilme = c.IdFilme,
                    NomeFilme = c.NomeFilme
                });

            return new ListAllFilmeResult(filmesResult);

        }
    }
}
