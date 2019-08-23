using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Domains
{
    public class FilmeDomain
    {
        public int IdFilme { get; set; }
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public GeneroDomain Genero { get; set; }
    }
}
