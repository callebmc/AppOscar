using MediatR;
using System;

namespace AppOscar.API.Domain
{
    public class CategoriaCreateCommand : IRequest<string>
    {
        public string NomeCategoria { get; }

        public int PontosCategoria { get; }

        public CategoriaCreateCommand(string nomeCategoria, int pontosCategoria)
        {
            if (string.IsNullOrWhiteSpace(nomeCategoria))
            {
                throw new ArgumentException("message", nameof(nomeCategoria));
            }
            NomeCategoria = nomeCategoria;
            PontosCategoria = pontosCategoria;
        }
    }
}
