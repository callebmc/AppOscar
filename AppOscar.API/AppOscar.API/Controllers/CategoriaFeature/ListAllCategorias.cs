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

namespace AppOscar.API.Controllers.CategoriaFeature
{
    public class ListAllCategorias : IRequest<ListAllCategoriasResult>
    {
    }

    public class ListAllCategoriasResult
    {
        public ListAllCategoriasResult(IEnumerable<Categoria> categorias)
        {
            Categorias = categorias ?? throw new ArgumentNullException(nameof(categorias));
        }

        public IEnumerable<Categoria> Categorias { get; }
    }

    public class ListAllCategoriasHandler : IRequestHandler<ListAllCategorias, ListAllCategoriasResult>
    {
        private readonly ILogger<ListAllCategoriasHandler> logger;
        private readonly AppOscarContext context;

        public ListAllCategoriasHandler(ILogger<ListAllCategoriasHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ListAllCategoriasResult> Handle(ListAllCategorias request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return ListAllCategoriasInternalAsync(cancellationToken);
        }

        private async Task<ListAllCategoriasResult> ListAllCategoriasInternalAsync(CancellationToken cancellationToken)
        {
            var categorias = await context.Categorias.ToListAsync();

            var categoriasResult = categorias.Select(c => new Categoria
            {
                IdCategoria = c.IdCategoria,
                NomeCategoria = c.NomeCategoria,
                PontosCategoria = c.PontosCategoria,
                CategoriaPhotoUrl = c.CategoriaPhotoUrl
            });

            return new ListAllCategoriasResult(categoriasResult);
        }
    }
}
