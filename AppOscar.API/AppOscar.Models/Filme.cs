using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    public class Filme
    {
        public Guid IdFilme { get; set; }

        public string NomeFilme { get; set; }

        public virtual ICollection<Participacao> Participantes { get; set; } = new HashSet<Participacao>();

        public virtual ICollection<Categoria> CategoriasVencidas { get; set; } = new HashSet<Categoria>();
    }
}
