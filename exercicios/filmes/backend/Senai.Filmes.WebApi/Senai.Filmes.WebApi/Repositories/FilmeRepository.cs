using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Repositories
{
    public class FilmeRepository
    {

        private string StringConexao = "Data Source=localhost; initial catalog=RoteiroFilmes;Integrated Security=true";

        public List<FilmeDomain> Listar()
        {
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT F.IdFilme, F.Titulo, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero;";

                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(sdr[0]),
                            Titulo = sdr[1].ToString(),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(sdr[2]),
                                Nome = sdr[3].ToString()
                            }
                        };

                        filmes.Add(filme);
                    }
                }
            }
            return filmes;
        }

        public FilmeDomain BuscarPorId(int id)
        {
            string Query = "SELECT F.IdFilme, F.Titulo, G.IdGenero, G.Nome FROM Filmes F INNER JOIN Generos G ON F.IdGenero = G.IdGenero WHERE F.IdFilme = @IdFilme;";


            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdFilme", id);
                    sdr = cmd.ExecuteReader();


                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            FilmeDomain filme = new FilmeDomain
                            {
                                IdFilme = Convert.ToInt32(sdr[0]),
                                Titulo = sdr[1].ToString(),
                                Genero = new GeneroDomain
                                {
                                    IdGenero = Convert.ToInt32(sdr[2]),
                                    Nome = sdr[3].ToString()
                                }
                            };

                            return filme;
                        }
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(FilmeDomain filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "INSERT INTO Filmes (Titulo, IdGenero) VALUES (@Titulo, @IdGenero)";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Titulo", filme.Titulo);
                cmd.Parameters.AddWithValue("@IdGenero", filme.GeneroId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
