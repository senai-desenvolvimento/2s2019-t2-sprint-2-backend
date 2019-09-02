using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class AutorRepository
    {
        // String de conexão
        private string StringConexao = "Data Source = localhost; Initial Catalog = BookStore; Integrated Security = True";
        //private string StringConexao = "Data Source=.\SqlExpress; Initial Catalog=BookStore;User Id=sa;Pwd=132";

        public List<AutorDomain> Listar()
        {
            // Declara a lista onde será armazenado os valores retornados
            List<AutorDomain> autores = new List<AutorDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT * FROM Autores";
                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = (bool) sdr["Ativo"],
                            DataNascimento = (DateTime) sdr["DataNascimento"]
                        };

                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }

        public void Cadastrar(AutorDomain autor)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "INSERT INTO Autores (Nome, Email, Ativo, DataNascimento) VALUES (@Nome, @Email, @Ativo, @DataNascimento)";
                // Declara um novo comando
                SqlCommand cmd = new SqlCommand(query, con);
                // Passa para o comando os parâmetros a serem alterados na string query
                cmd.Parameters.AddWithValue("@Nome", autor.Nome);
                cmd.Parameters.AddWithValue("@Email", autor.Email);
                cmd.Parameters.AddWithValue("@Ativo", autor.Ativo);
                cmd.Parameters.AddWithValue("@DataNascimento", autor.DataNascimento);

                // Abre a conexão
                con.Open();
                //Executa o comado
                cmd.ExecuteNonQuery();
            }
        }

        public AutorDomain BuscarLivrosPorAutor(int IdAutor)
        {
            AutorDomain autor = new AutorDomain();
            List<LivroDomain> livros = new List<LivroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT A.IdAutor, A.Nome, A.Email, A.DataNascimento, A.Ativo, L.IdLivro, L.Titulo, L.IdGenero, G.Descricao FROM Autores A JOIN Livros L ON A.IdAutor = L.IdAutor JOIN Generos G ON L.IdGenero = G.IdGenero WHERE L.IdAutor = @IdAutor";

                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", IdAutor);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        autor.IdAutor = Convert.ToInt32(sdr["IdAutor"]);
                        autor.Nome = sdr["Nome"].ToString();
                        autor.Email = sdr["Email"].ToString();
                        autor.Ativo = (bool)sdr["Ativo"];
                        autor.DataNascimento = (DateTime)sdr["DataNascimento"];                   
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Descricao"].ToString()
                            }
                        };
                        livros.Add(livro);
                    }
                    autor.livros = livros;
                }
            }
            return autor;
        }

        public AutorDomain BuscarLivrosPorAutorAtivo(int IdAutor)
        {
            AutorDomain autor = new AutorDomain();
            List<LivroDomain> livros = new List<LivroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT A.IdAutor, A.Nome, A.Email, A.DataNascimento, A.Ativo, L.IdLivro, L.Titulo, L.IdGenero, G.Descricao FROM Autores A JOIN Livros L ON A.IdAutor = L.IdAutor JOIN Generos G ON L.IdGenero = G.IdGenero WHERE A.Ativo = 'true' and L.IdAutor = @IdAutor";

                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", IdAutor);
                    sdr = cmd.ExecuteReader();                       

                    while (sdr.Read())
                    {
                        autor.IdAutor = Convert.ToInt32(sdr["IdAutor"]);
                        autor.Nome = sdr["Nome"].ToString();
                        autor.Email = sdr["Email"].ToString();
                        autor.Ativo = (bool)sdr["Ativo"];
                        autor.DataNascimento = (DateTime)sdr["DataNascimento"];
                        LivroDomain livro = new LivroDomain
                        {
                            IdLivro = Convert.ToInt32(sdr["IdLivro"]),
                            Titulo = sdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Descricao = sdr["Descricao"].ToString()
                            }
                        };
                        livros.Add(livro);
                    }
                    if (!autor.Ativo)
                        return null;
                    autor.livros = livros;
                }
            }
            return autor;
        }

        public List<AutorDomain> BuscarAutoresAtivos()
        {
            // Declara a lista onde será armazenado os valores retornados
            List<AutorDomain> autores = new List<AutorDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT * FROM Autores WHERE Ativo = 1";
                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = (bool)sdr["Ativo"],
                            DataNascimento = (DateTime)sdr["DataNascimento"]
                        };

                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }

        public List<AutorDomain> BuscarPorAnoDeNascimento(int ano)
        {
            List<AutorDomain> autores = new List<AutorDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM Autores WHERE YEAR(DataNascimento) = @ano";

                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ano", ano);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        AutorDomain autor = new AutorDomain
                        {
                            IdAutor = Convert.ToInt32(sdr["IdAutor"]),
                            Nome = sdr["Nome"].ToString(),
                            Email = sdr["Email"].ToString(),
                            Ativo = (bool)sdr["Ativo"],
                            DataNascimento = (DateTime)sdr["DataNascimento"]
                        };

                        autores.Add(autor);
                    }
                }
            }
            return autores;
        }
    }
}
