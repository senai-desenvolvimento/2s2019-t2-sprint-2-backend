using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class UsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Usuarios usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (usuario == null)
                    return null;
                return usuario;
            }
        }
    }
}
