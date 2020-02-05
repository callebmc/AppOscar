using System;
using System.Collections.Generic;

namespace AppOscar.Models
{
    /// <summary>
    /// Representação da Participação
    /// </summary>
    public class Participacao
    {
        /// <summary>
        /// Identificador unico da Participação
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador unico para Categorias
        /// </summary>
        public Guid IdCategoria { get; set; }

        /// <summary>
        /// Referência para Categorias
        /// </summary>
        public virtual Categoria Categoria { get; set; }

        /// <summary>
        /// Identificador para Filmes
        /// </summary>
        public Guid IdFilme { get; set; }

        /// <summary>
        /// Referência para Filmes
        /// </summary>
        public virtual Filme Filme { get; set; }

        /// <summary>
        /// Votos lançados para esta participação.
        /// </summary>
        public virtual ICollection<Voto> Votos { get; set; } = new HashSet<Voto>();
    }
}