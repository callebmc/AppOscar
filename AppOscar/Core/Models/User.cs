using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class User
    {
        public User() { }

        public int id_user { get; set; }

        public string nomeUsuario { get; set; }

        public string emailUsuario { get; set; }

        public string senhaUsuario { get; set; }
    }
}
