using MediatR;
using System;

namespace AppOscar.API.Domain
{
    public class CategoriaCreateCommand : IRequest<string>
    {
        public Guid Id;

        public string NomeCategoria;

        public int PontosCategoria;
    }
}
