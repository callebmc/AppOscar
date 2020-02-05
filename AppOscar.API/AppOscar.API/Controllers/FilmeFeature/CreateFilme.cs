using AppOscar.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.FilmeFeature
{
    public class CreateFilme : IRequest<CreateFilmeResult>
    {
        [Required]
        public string NomeFilme { get; set; }

        [Required]
        public string FilmePhotoUrl { get; set; }
    }

    public class CreateFilmeResult
    {
        public DateTime DthCriacao { get; set; }

        public Guid IdFilme { get; set; }
    }

    public class CreateFilmeHandler : IRequestHandler<CreateFilme, CreateFilmeResult>
    {
        private readonly ILogger<CreateFilmeHandler> logger;
        private readonly AppOscarContext context;

        public CreateFilmeHandler(ILogger<CreateFilmeHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CreateFilmeResult> Handle(CreateFilme request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return CreateFilmeInternalAsync(request.NomeFilme, request.FilmePhotoUrl, cancellationToken);
        }

        private async Task<CreateFilmeResult> CreateFilmeInternalAsync(string nomeFilme, string filmePhotoUrl, CancellationToken cancellationToken)
        {
            var novoFilme = new Models.Filme { NomeFilme = nomeFilme, FilmePhotoUrl = filmePhotoUrl };
            context.Filmes.Add(novoFilme);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateFilmeResult { DthCriacao = DateTime.Now, IdFilme = novoFilme.IdFilme };
        }
    }
}
