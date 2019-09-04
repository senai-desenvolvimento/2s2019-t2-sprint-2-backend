using System;
using System.Collections.Generic;

namespace Senai.CgStore.WebApi.Domains
{
    public partial class Permissoes
    {
        public Permissoes()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int PermissaoId { get; set; }
        public string Nome { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
