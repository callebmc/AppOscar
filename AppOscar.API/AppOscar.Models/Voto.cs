using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    public class Voto
    {
        public int idVoto { get; set; }

        public ICollection<Categoria> Categorias { get; set; }

        public int idCategoria { get; set; }
    }
}
