using Senai.Sstop.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Repositories
{
    // acesso ao banco de dados
    public class ArtistaRepository
    {
        private string StringConexao = "Data Source=localhost; initial catalog=M_SStop; Integrated Security=true";

        public List<ArtistaDomain> Listar()
        {
            List<ArtistaDomain> artistas = new List<ArtistaDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT A.IdArtista, A.Nome, A.IdEstiloMusical, E.Nome AS NomeEstilo FROM Artistas A INNER JOIN EstilosMusicais E ON A.IdEstiloMusical = E.IdEstiloMusical;";

                // abre a conexao
                con.Open();

                SqlDataReader sdr;

                // declara o comando
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // executa a query
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        ArtistaDomain artista = new ArtistaDomain
                        {
                            IdArtista = Convert.ToInt32(sdr["IdArtista"]),
                            Nome = sdr["Nome"].ToString(),
                            Estilo = new EstiloDomain
                            {
                                IdEstilo = Convert.ToInt32(sdr["IdEstiloMusical"]),
                                Nome = sdr["NomeEstilo"].ToString()
                            }
                        };
                        artistas.Add(artista);
                    }

                }
            }
            return artistas;
        }
    }
}
