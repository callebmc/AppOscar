using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.Domain
{
    public class FilmeCreateCommand : IRequest<string>
    {
        public Guid IdFilme;

        public string NomeFilme;
    }
}
