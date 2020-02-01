using System;

namespace AppOscar.Models
{
    /// <summary>
    /// Voto de um usuário sobre a participação de um filme em uma categoria.
    /// </summary>
    public class Voto
    {
        public int Id { get; set; }

        public DateTimeOffset DthCriacao { get; set; }

        public int IdParticipacao { get; set; }

        public virtual Participacao Participacao { get; set; }
    }
}
