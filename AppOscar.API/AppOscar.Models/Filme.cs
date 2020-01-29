using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    /// <summary>
    /// Classe de Filme
    /// </summary>
    public class Filme
    {
        /// <summary>
        /// Identificador unico de FIlme
        /// </summary>
        public Guid IdFilme { get; set; }

        /// <summary>
        /// Nome do filme
        /// </summary>
        public string NomeFilme { get; set; }

        /// <summary>
        /// Referência para Participantes
        /// </summary>
        public virtual ICollection<Participacao> Participantes { get; set; } = new HashSet<Participacao>();


        /// <summary>
        /// Referência para as Categorias Vencidas pelo filme
        /// </summary>
        public virtual ICollection<Categoria> CategoriasVencidas { get; set; } = new HashSet<Categoria>();
    }
}
