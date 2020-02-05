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
    public class ListCategoriasPorFilme : IRequest<ListCategoriasPorFilmeResult>
    {
        public Guid FilmeId { get; set; }
    }

    public class ListCategoriasPorFilmeResult
    {
        public ListCategoriasPorFilmeResult(IEnumerable<Categoria> categorias)
        {
            Categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));
        }

        public IEnumerable<Categoria> Categorias { get; }
    }

    public class ListCategoriaPorFilmeHandler : IRequestHandler<ListCategoriasPorFilme, ListCategoriasPorFilmeResult>
    {
        private readonly ILogger<ListCategoriaPorFilmeHandler> logger;
        private readonly AppOscarContext context;

        public ListCategoriaPorFilmeHandler(ILogger<ListCategoriaPorFilmeHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListCategoriasPorFilmeResult> Handle(ListCategoriasPorFilme request, CancellationToken ct)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListCategoriasPorFilmeInternalAsync(request.FilmeId, ct);
        }

        private async Task<ListCategoriasPorFilmeResult> ListCategoriasPorFilmeInternalAsync(Guid filmeId, CancellationToken ct)
        {
            var filmes = await context.Filmes
                .AsNoTracking()
                .Include(c => c.Participantes)
                    .ThenInclude(p => p.Categoria)
                .SingleOrDefaultAsync(c => c.IdFilme == filmeId, ct);

            if (filmes is null)
                throw new KeyNotFoundException("Filme não encontrado");

            var categoriasParticipantes = filmes.Participantes
                .Select(p => p.Categoria)
                .Select(c => new Categoria
                {
                    IdCategoria = c.IdCategoria,
                    NomeCategoria = c.NomeCategoria,
                    PontosCategoria = c.PontosCategoria,
                    CategoriaPhotoUrl = c.CategoriaPhotoUrl
                });

            return new ListCategoriasPorFilmeResult(categoriasParticipantes);
        }

    }
}
