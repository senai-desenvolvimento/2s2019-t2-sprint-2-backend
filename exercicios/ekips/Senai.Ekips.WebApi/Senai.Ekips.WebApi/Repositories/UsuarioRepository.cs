using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{

    public class UsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel loginViewModel)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                Usuarios usuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Senha == loginViewModel.Senha);
                if (usuarioBuscado == null)
                    return null;
                return usuarioBuscado;
            }
        }
    }

}