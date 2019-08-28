using System;
using System.Collections.Generic;

namespace Senai.Optus.WebApi.Domains
{
    public partial class Estilos
    {
        public Estilos()
        {
            //Artistas = new HashSet<Artistas>();
        }

        public int IdEstilo { get; set; }
        public string Nome { get; set; }

        public ICollection<Artistas> Artistas { get; set; }

        internal Estilos Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
