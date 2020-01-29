using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    /// <summary>
    /// Classe de Categoria
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Identificador unico da categoria
        /// </summary>
        public Guid IdCategoria { get; set; }

        /// <summary>
        /// Nome da caategoria
        /// </summary>
        public string NomeCategoria { get; set; }

        /// <summary>
        /// Quantidade de pontos que vale a categoria
        /// </summary>
        public int PontosCategoria { get; set; }

        /// <summary>
        /// Identificador do filme vencedor da Categoria
        /// </summary>
        public Guid? FilmeVencedorId { get; set; }

        /// <summary>
        /// Referência para o Filme
        /// </summary>
        public virtual Filme FilmeVencedor { get; set; }

        /// <summary>
        /// Referência para Participantes
        /// </summary>
        public virtual ICollection<Participacao> Participantes { get; set; }
    }
}
