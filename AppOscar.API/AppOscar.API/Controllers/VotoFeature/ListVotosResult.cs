using AppOscar.Models;
using System;
using System.Collections.Generic;

namespace AppOscar.API.Controllers.VotoFeature
{
    public class ListVotosResult
    {
        public ListVotosResult(IEnumerable<Voto> votos)
        {
            Votos = votos ?? throw new ArgumentNullException(nameof(votos));
        }

        public IEnumerable<Voto> Votos { get; }
    }
}
