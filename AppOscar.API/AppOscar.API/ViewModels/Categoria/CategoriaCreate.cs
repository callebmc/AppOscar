using System;

namespace AppOscar.API.ViewModels.Categoria
{
    public class CategoriaCreate
    {
        /// <summary>
        /// Identificador unico de Categoria
        /// </summary>
        public Guid Id;


        /// <summary>
        /// Nome da Categoria
        /// </summary>
        public string NomeCategoria;


        /// <summary>
        /// Pontos que a categoria vale
        /// </summary>
        public int PontosCategoria;
    }
}
