using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOscar.API.ViewModels.Filme
{
    public class FilmeCreate
    {
        /// <summary>
        /// Identificador Unico de Filme
        /// </summary>
        public Guid IdFilme { get; set; }

        /// <summary>
        /// Nome do filme
        /// </summary>
        public string NomeFilme { get; set; }
    }
}
