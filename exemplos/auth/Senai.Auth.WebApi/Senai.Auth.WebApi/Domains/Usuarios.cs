using System;
using System.Collections.Generic;

namespace Senai.Auth.WebApi.Domains
{
    public partial class Usuarios
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? PermissaoId { get; set; }

        public Permissoes Permissao { get; set; }
    }
}
