using AppOscar.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppOscar.API.Controllers.VotoFeature
{
    public class CheckVoto : IRequest<CheckVotoResult>
    {
        public CheckVoto(string idUsuario)
        {
            IdUsuario = idUsuario;
        }

        [Required(AllowEmptyStrings = false)]
        public string IdUsuario { get; set; }
    }

    public class CheckVotoResult
    {
        public CheckVotoResult(bool jaVotou)
        {
            JaVotou = jaVotou;
        }

        public bool JaVotou { get; }
    }

    public class CheckVotoHandler : IRequestHandler<CheckVoto, CheckVotoResult>
    {
        private readonly AppOscarContext context;
        public CheckVotoHandler(AppOscarContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CheckVotoResult> Handle(CheckVoto request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new ArgumentNullException(nameof(request));

            return CheckVotoInternalAsync(request.IdUsuario, cancellationToken);
        }

        private async Task<CheckVotoResult> CheckVotoInternalAsync(string idUsuario, CancellationToken cancellationToken)
        {

            var findUser = context.Votos.Any(x => x.IdUsuario == idUsuario);

            if (findUser)
                return new CheckVotoResult(findUser);

            return new CheckVotoResult(findUser);
        }
    }
}
