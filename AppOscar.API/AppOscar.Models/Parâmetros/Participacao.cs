using System;

namespace AppOscar.Models
{
    public class Participacao
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
        public Guid IdFilme { get; set; }
        public virtual Filme Filme { get; set; }
    }
}