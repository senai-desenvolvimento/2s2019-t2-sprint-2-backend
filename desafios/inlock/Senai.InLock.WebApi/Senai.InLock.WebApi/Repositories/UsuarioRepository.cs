using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class UsuarioRepository
    {
        /// <summary>
        /// Buscar um usuario por email e senha
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>Usuario Encontrado</returns>
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Usuarios UsuarioBuscado = ctx.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (UsuarioBuscado == null)
                {
                    return null;
                }
                return UsuarioBuscado;
            }
        }
    }
}
