using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class LivroRepository
    {
        // String de conexão
        private string StringConexao = "Data Source = localhost; Initial Catalog = BookStore; Integrated Security = True";
        //private string StringConexao = "Data Source=.\SqlExpress; Initial Catalog=BookStore;User Id=sa;Pwd=132";

        public List<LivroDomain> Listar()
        {
            // Declara a lista onde será armazenado os valores retornados
            List<LivroDomain> livros = new List<LivroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT L.IdLivro, L.Titulo, L.IdAutor, L.IdGenero, " +
                    "A.Nome, A.Email, A.DataNascimento, A.Ativo," +
                    "G.Descricao " +
                    "FROM Livros L " +
                    "JOIN Autores A ON L.IdAutor = A.IdAutor " +
                    "JOIN Generos G ON L.IdGenero = G.IdGenero";

                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Nome"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = (bool)sdr["Ativo"],
                                DataNascimento = (DateTime)sdr["DataNascimento"]
                            },
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Descricao"].ToString()
                            }


                        };

                        livros.Add(livro);
                    }
                }
            }
            return livros;
        }

        public LivroDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                string query = "SELECT L.IdLivro, L.Titulo, L.IdAutor, L.IdGenero, " +
                     "A.Nome, A.Email, A.DataNascimento, A.Ativo," +
                     "G.Descricao " +
                     "FROM Livros L " +
                     "JOIN Autores A ON L.IdAutor = A.IdAutor " +
                     "JOIN Generos G ON L.IdGenero = G.IdGenero where L.IdLivro = @IdLivro";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdLivro", id);

                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Autor = new AutorDomain
                            {
                                IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                                Nome = sdr["Nome"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Ativo = (bool)sdr["Ativo"],
                                DataNascimento = (DateTime)sdr["DataNascimento"]
                            },
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Descricao"].ToString()
                            }
                        };
                        return livro;
                    }
                }
            }
            return null;
        }

        public void Cadastrar(LivroDomain livro)
        {
            // Declara a instrução a ser executada
            string query = "INSERT INTO Livros (Titulo, IdGenero, IdAutor) VALUES (@Titulo, @IdGenero, @IdAutor)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara um novo comando
                SqlCommand cmd = new SqlCommand(query, con);

                // Passa para o comando os parâmetros a serem alterados na string query
                cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", livro.IdGenero);
                cmd.Parameters.AddWithValue("@IdAutor", livro.IdAutor);

                // Abre a conexão
                con.Open();
                //Executa o comado
                cmd.ExecuteNonQuery();

            }
        }

        public void Atualizar(LivroDomain livro)
        {
            string query = "UPDATE Livros SET Titulo = @Titulo, IdAutor = @IdAutor, IdGenero = @IdGenero WHERE IdLivro = @IdLivro";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara um novo comando
                SqlCommand cmd = new SqlCommand(query, con);

                // Passa para o comando os parâmetros a serem alterados na string query
                cmd.Parameters.AddWithValue("@IdLivro", livro.IdLivro);
                cmd.Parameters.AddWithValue("@Titulo", livro.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", livro.IdGenero);
                cmd.Parameters.AddWithValue("@IdAutor", livro.IdAutor);

                // Abre a conexão
                con.Open();
                //Executa o comado
                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "DELETE FROM Livros WHERE IdLivro = @IdLivro";
                // Declara um novo comando
                SqlCommand cmd = new SqlCommand(query, con);

                // Passa para o comando os parâmetros a serem alterados na string query
                cmd.Parameters.AddWithValue("IdLivro", id);

                // Abre a conexão
                con.Open();
                //Executa o comado
                cmd.ExecuteNonQuery();
            }
        }

    }
}
