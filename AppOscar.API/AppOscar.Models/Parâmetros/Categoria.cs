using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    public class Categoria
    {
        public Guid IdCategoria { get; set; }

        public string NomeCategoria { get; set; }

        public int PontosCategoria { get; set; }

        public Guid? FilmeVencedorId { get; set; }

        public virtual Filme FilmeVencedor { get; set; }

        public virtual ICollection<Participacao> Participantes { get; set; }
    }
}
