using Senai.Sstop.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Repositories
{
    public class EstiloRepository
    {
        //List<EstiloDomain> estilos = new List<EstiloDomain>()
        //{
        //    new EstiloDomain { IdEstilo = 1, Nome = "Rock" }
        //    ,new EstiloDomain { IdEstilo = 2, Nome = "Alternativo" }
        //};

        private string StringConexao = "Data Source=localhost; initial catalog=M_SStop; Integrated Security=true";

        // private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=M_SStop;User Id=sa;Pwd=132;";

        public List<EstiloDomain> Listar()
        {

            List<EstiloDomain> estilos = new List<EstiloDomain>();

            // abrir uma conexao com o banco
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // fazer a leitura de todos os registros
                // declarar a instrucao a ser realizada
                string Query = "SELECT IdEstiloMusical, Nome FROM EstilosMusicais ORDER BY Nome DESC";

                // abre a conexao com o bd
                con.Open();
                // declaro para percorrer a lista
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // executa a query
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstiloDomain estilo = new EstiloDomain
                        {
                            IdEstilo = Convert.ToInt32(rdr["IdEstiloMusical"]),
                            Nome = rdr["Nome"].ToString()
                        };
                        estilos.Add(estilo);
                    }

                }

            }

            // devolver a lista preenchida
            return estilos;
        }
    }
}
