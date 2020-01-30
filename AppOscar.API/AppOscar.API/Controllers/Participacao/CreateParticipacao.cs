using AppOscar.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.Participacao
{
    public class CreateParticipacao : IRequest<CreateParticipacaoResult>
    {
        [Required]
        public Guid IdFilme { get; set; }

        [Required]
        public Guid IdCategoria { get; set; }
    }

    public class CreateParticipacaoResult
    {
        public DateTime DthCriacao { get; set; }

        public int IdParticipacao { get; set; }
    }

    public class CreateParticipacaoHandler : IRequestHandler<CreateParticipacao, CreateParticipacaoResult>
    {
        private readonly ILogger<CreateParticipacaoHandler> logger;
        private readonly AppOscarContext context;

        public CreateParticipacaoHandler(ILogger<CreateParticipacaoHandler> logger, AppOscarContext context)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CreateParticipacaoResult> Handle(CreateParticipacao request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return CreateParticipacaoInternalAsync(request.IdCategoria, request.IdFilme, cancellationToken);
        }

        private async Task<CreateParticipacaoResult> CreateParticipacaoInternalAsync(Guid idCategoria, Guid idFilme, CancellationToken cancellationToken)
        {
            // Validar que a Categoria existe
            var categoria = await context.Categorias.FindAsync(new object[] { idCategoria }, cancellationToken);
            if (categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            // Validar que o Filme existe
            if ((await context.Filmes.FindAsync(new object[] { idFilme }, cancellationToken)) is null)
                throw new KeyNotFoundException("Filme não encontrado");

            // Inserir a Partipacao através da Categoria
            var novaParticipacao = new Models.Participacao { IdCategoria = idCategoria, IdFilme = idFilme };
            categoria.Participantes.Add(novaParticipacao);

            // Salvar Alterações
            await context.SaveChangesAsync(cancellationToken);

            // Retornar o result
            return new CreateParticipacaoResult { DthCriacao = DateTime.Now, IdParticipacao = novaParticipacao.Id };
        }
    }
}
