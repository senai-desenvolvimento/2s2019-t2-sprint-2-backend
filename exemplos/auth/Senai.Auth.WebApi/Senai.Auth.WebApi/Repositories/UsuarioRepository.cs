using Microsoft.EntityFrameworkCore;
using Senai.Auth.WebApi.Domains;
using Senai.Auth.WebApi.ViewModels;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Senai.Auth.WebApi.Repositories
{
    public class UsuarioRepository
    {
        public Usuarios BuscarPorEmailESenha(LoginViewModel login)
        {
            using (AuthContext ctx = new AuthContext())
            {
                // buscar os dados no banco e verificar se este email e senha sao validos
                Usuarios UsuarioBuscado = ctx.Usuarios.Include(x => x.Permissao).FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);
                // neste cenario, precisamos incluir no join a permissao, para que tenhamos acesso ao nome dela, e nao somente ao id
                if (UsuarioBuscado == null)
                {
                    return null;
                }
                return UsuarioBuscado;
            }
        }

        /// <summary>
        /// Buscando por email e senha atraves do sqlclient.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Usuarios BuscarPorEmailESenhaSqlClient(LoginViewModel login)
        {
            string StringConexao = "Data Source=localhost; initial catalog=E_Auth;integrated security=true"; 

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string QuerySelect = "SELECT U.UsuarioId, U.Email, U.Senha, P.PermissaoId, P.Nome AS NomePermissao FROM Usuarios U INNER JOIN Permissoes P ON U.PermissaoId = P.PermissaoId WHERE U.Email = @Email AND U.Senha = @Senha";

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", login.Email);
                    cmd.Parameters.AddWithValue("@Senha", login.Senha);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Usuarios usuario = new Usuarios
                            {
                                UsuarioId = Convert.ToInt32(sdr["UsuarioId"]),
                                Email = sdr["Email"].ToString(),
                                Permissao = new Permissoes
                                {
                                    PermissaoId = Convert.ToInt32(sdr["PermissaoId"]),
                                    Nome = sdr["NomePermissao"].ToString()
                                }
                            };
                            return usuario;
                        }
                    }
                }
                return null;
            }
        }

    }
}
