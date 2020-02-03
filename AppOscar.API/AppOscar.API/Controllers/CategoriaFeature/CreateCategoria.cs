using AppOscar.Models;
using AppOscar.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.CategoriaFeature
{
    public class CreateCategoria : IRequest<CreateCategoriaResult>
    {
        [Required]
        public string NomeCategoria { get; set; }

        [Required]
        public int PontosCategoria { get; set; }

        [Required]
        public string CategoriaPhotoUrl { get; set; }
    }

    public class CreateCategoriaResult
    {
        public DateTime DthCriacao { get; set; }

        public Guid IdCategoria { get; set; }
    }

    public class CreateCategoriaHandler : IRequestHandler<CreateCategoria, CreateCategoriaResult>
    {
        private readonly ILogger<CreateCategoriaHandler> logger;
        private readonly AppOscarContext context;

        public CreateCategoriaHandler(ILogger<CreateCategoriaHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CreateCategoriaResult> Handle(CreateCategoria request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateCategoriaInternalAsync(request.NomeCategoria, request.PontosCategoria, request.CategoriaPhotoUrl, cancellationToken);
        }

        private async Task<CreateCategoriaResult> CreateCategoriaInternalAsync(string nomeCategoria, int pontosCategoria, string categoriaPhotoUrl, CancellationToken cancellationToken)
        {
            var novaCategoria = new Categoria { NomeCategoria = nomeCategoria, PontosCategoria = pontosCategoria, CategoriaPhotoUrl = categoriaPhotoUrl};
            context.Categorias.Add(novaCategoria);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateCategoriaResult { DthCriacao = DateTime.Now, IdCategoria = novaCategoria.IdCategoria };
        }
    }
}
