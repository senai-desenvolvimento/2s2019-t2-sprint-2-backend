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
        public Usuarios BuscarPorEmailESenha(LoginViewModel loginViewModel)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Usuarios usuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Senha == loginViewModel.Senha);
                if (usuarioBuscado == null)
                    return null;
                return usuarioBuscado;
            }
                
        }
    }
}
