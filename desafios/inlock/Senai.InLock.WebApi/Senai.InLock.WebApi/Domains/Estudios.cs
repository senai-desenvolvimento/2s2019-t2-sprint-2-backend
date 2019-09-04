using System;
using System.Collections.Generic;

namespace Senai.InLock.WebApi.Domains
{
    public partial class Estudios
    {
        public Estudios()
        {
            Jogos = new HashSet<Jogos>();
        }

        public int EstudioId { get; set; }
        public string NomeEstudio { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? UsuarioId { get; set; }
        public string PaisOrigem { get; set; }

        public Usuarios Usuario { get; set; }
        public ICollection<Jogos> Jogos { get; set; }
    }
}
