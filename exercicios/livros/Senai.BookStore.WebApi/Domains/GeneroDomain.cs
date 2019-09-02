using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Domains
{
    public class GeneroDomain
    {
        public int IdGenero { get; set; }
        public string Descricao { get; set; }

        public List<LivroDomain> livros { get; set; }
    }
}
