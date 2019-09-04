using Microsoft.EntityFrameworkCore;
using Senai.CgStore.WebApi.Domains;
using Senai.CgStore.WebApi.Interfaces;
using Senai.CgStore.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.CgStore.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (CgStoreContext ctx = new CgStoreContext())
            {
                Usuarios usuario = ctx.Usuarios.Include(x => x.Permissao).FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (usuario == null)
                    return null;
                return usuario;
            }
        }
    }
}
