using System;
using System.Collections.Generic;
using System.Text;

namespace AppOscar.Models
{
    public class Filme
    {
        public int idFilme { get; set; }

        public string nomeFilme { get; set; }

        public bool venceu { get; set; } = false;
    }
}
