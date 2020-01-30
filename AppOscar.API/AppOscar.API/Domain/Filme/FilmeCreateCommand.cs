using MediatR;
using System;

namespace AppOscar.API.Domain
{
    public class FilmeCreateCommand : IRequest<string>
    {
        public Guid IdFilme;

        public string NomeFilme;
    }
}
