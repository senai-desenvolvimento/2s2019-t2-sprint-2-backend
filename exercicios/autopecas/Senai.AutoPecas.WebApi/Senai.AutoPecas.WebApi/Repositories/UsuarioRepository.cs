using Microsoft.EntityFrameworkCore;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Repositories
{
    public class UsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (AutoPecasContext ctx = new AutoPecasContext())
            {
                // buscar os dados no banco e verificar se este email e senha sao validos
                Usuarios UsuarioBuscado = ctx.Usuarios.Include(x => x.Fornecedores).FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                if (UsuarioBuscado == null)
                {
                    return null;
                }
                return UsuarioBuscado;
            }
        }
    }
}
