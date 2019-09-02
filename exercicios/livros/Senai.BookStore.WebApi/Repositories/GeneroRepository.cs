using Senai.BookStore.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.BookStore.WebApi.Repositories
{
    public class GeneroRepository
    {
        // String de conexão
        private string StringConexao = "Data Source = localhost; Initial Catalog = BookStore; Integrated Security = True";
        //private string StringConexao = "Data Source=.\SqlExpress; Initial Catalog=BookStore;User Id=sa;Pwd=132";

        public List<GeneroDomain> Listar()
        {
            // Declara a lista onde será armazenado os valores retornados
            List<GeneroDomain> generos = new List<GeneroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT * FROM Generos";
                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                            Descricao = sdr["Descricao"].ToString()
                        };

                        generos.Add(genero);
                    }
                }
            }
            return generos;
        }

        public void Cadastrar(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "INSERT INTO Generos (Descricao) VALUES (@Descricao)";
                // Declara um novo comando
                SqlCommand cmd = new SqlCommand(query, con);
                // Passa para o comando os parâmetros a serem alterados na string query
                cmd.Parameters.AddWithValue("@Descricao", genero.Descricao);
                // Abre a conexão
                con.Open();
                //Executa o comado
                cmd.ExecuteNonQuery();
            }
        }

        public GeneroDomain BuscarLivroPorGenero(string nome)
        {
            GeneroDomain genero = new GeneroDomain();
            List<LivroDomain> livros = new List<LivroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string query = "SELECT G.IdGenero, G.Descricao, L.IdLivro, L.Titulo, L.IdAutor, A.Nome, A.Email, A.DataNascimento, A.Ativo FROM Generos G JOIN Livros L ON L.IdGenero = G.IdGenero JOIN Autores A ON A.IdAutor = L.IdAutor WHERE G.Descricao = @nome";
                // Abre a conexão
                con.Open();
                // Declara um SqlDataReader que armazenará os valores recuperados
                SqlDataReader sdr;


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        genero.IdGenero = Convert.ToInt32(sdr["IdGenero"]);
                        genero.Descricao = sdr["Descricao"].ToString();
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
                            }
                        };
                        livros.Add(livro);
                    }
                    if (genero.IdGenero == 0)
                        return null;

                    genero.livros = livros;
                }
            }
            return genero;
        }
    }
}
