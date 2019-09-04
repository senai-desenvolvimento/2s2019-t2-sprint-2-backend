using System;
using System.Collections.Generic;

namespace Senai.CgStore.WebApi.Domains
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Categorias = new HashSet<Categorias>();
        }

        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? PermissaoId { get; set; }

        public Permissoes Permissao { get; set; }
        public ICollection<Categorias> Categorias { get; set; }
    }
}
