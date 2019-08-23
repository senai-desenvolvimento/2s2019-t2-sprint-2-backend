using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories
{
    public class GeneroRepository
    {
        private string StringConexao = "Data Source=localhost; initial catalog=RoteiroFilmes;Integrated Security=true";

        public List<GeneroDomain> Listar()
        {
            List<GeneroDomain> generos = new List<GeneroDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT IdGenero, Nome FROM Generos";

                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(sdr[0]),
                            Nome = sdr[1].ToString()
                        };

                        generos.Add(genero);
                    }
                }
            }
            return generos;
        }

        public GeneroDomain ListarFilmesDoGenero(int id)
        {
            GeneroDomain genero = new GeneroDomain();
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT F.IdFilme, F.Titulo, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE G.IdGenero = @IdGenero;";

                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            genero.IdGenero = Convert.ToInt32(sdr[2]);
                            genero.Nome = sdr[3].ToString();
                            FilmeDomain filme = new FilmeDomain()
                            {
                                IdFilme = Convert.ToInt32(sdr[0]),
                                Titulo = sdr[1].ToString()
                            };
                            filmes.Add(filme);
                        }
                        genero.Filmes = filmes;
                    }
                }
            }
            return genero;
        }

        /// <summary>
        /// Buscar um determinado genero por id.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Retorna a lista de generos.</returns>
        public GeneroDomain BuscarPorId(int id)
        {
            string Query = "SELECT IdGenero, Nome FROM Generos WHERE IdGenero = @IdGenero";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    sdr = cmd.ExecuteReader();


                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            GeneroDomain genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr["IdGenero"]),
                                Nome = sdr["Nome"].ToString()
                            };

                            return genero;
                        }
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastrar um novo genero
        /// </summary>
        /// <param name="generoDomain">GeneroDomain</param>
        public void Cadastrar(GeneroDomain generoDomain)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "INSERT INTO Generos (Nome) VALUES (@Nome)";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", generoDomain.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Atualizar um genero existente
        /// </summary>
        /// <param name="genero">GeneroDomain</param>
        public void Alterar(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "UPDATE Generos SET Nome = @Nome WHERE IdGenero = @IdGenero";

                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);
                cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletar um genero.
        /// </summary>
        /// <param name="id">id</param>
        public void Deletar(int id)
        {
            string Query = "DELETE FROM Generos WHERE IdGenero = @IdGenero;";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }



    }
}
