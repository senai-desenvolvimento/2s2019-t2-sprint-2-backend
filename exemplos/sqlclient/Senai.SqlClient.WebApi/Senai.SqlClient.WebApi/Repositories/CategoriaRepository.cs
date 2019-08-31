using Senai.SqlClient.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SqlClient.WebApi.Repositories
{
    public class CategoriaRepository
    {
        // declarar a string de conexao de acordo com o bd a ser utilizado
        private string StringConexao = "Data Source=localhost; initial catalog=E_SqlClient; Integrated Security=true";

        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns>Lista de categorias</returns>
        public List<CategoriaDomain> Listar()
        {

            List<CategoriaDomain> categorias = new List<CategoriaDomain>();

            // abrir uma conexao com o banco
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // fazer a leitura de todos os registros
                // declarar a instrucao a ser realizada
                string Query = "SELECT CategoriaId, Nome, DataCriacao, Ativo FROM Categorias";

                // abre a conexao com o bd
                con.Open();
                // declaro para percorrer a lista
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // executa a query
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        CategoriaDomain categoria = new CategoriaDomain
                        {
                            CategoriaId = Convert.ToInt32(sdr["CategoriaId"]),
                            Nome = sdr["Nome"].ToString(),
                            DataCriacao = Convert.ToDateTime(sdr["DataCriacao"].ToString()),
                            Ativo = Convert.ToBoolean(sdr["Ativo"].ToString())
                        };
                        categorias.Add(categoria);
                    }

                }

            }

            // devolver a lista preenchida
            return categorias;
        }

        /// <summary>
        /// Buscar todas as categorias por nome
        /// </summary>
        /// <param name="nome">nome</param>
        /// <returns>lista de categorias</returns>
        public List<CategoriaDomain> BuscarPorNome(string nome)
        {

            List<CategoriaDomain> categorias = new List<CategoriaDomain>();

            // abrir uma conexao com o banco
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // fazer a leitura de todos os registros
                // declarar a instrucao a ser realizada
                string Query = "SELECT CategoriaId, Nome, DataCriacao, Ativo FROM Categorias WHERE Nome LIKE '%"+ nome + "%'";

                // abre a conexao com o bd
                con.Open();
                // declaro para percorrer a lista
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // executa a query
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        CategoriaDomain categoria = new CategoriaDomain
                        {
                            CategoriaId = Convert.ToInt32(sdr["CategoriaId"]),
                            Nome = sdr["Nome"].ToString(),
                            DataCriacao = Convert.ToDateTime(sdr["DataCriacao"].ToString()),
                            Ativo = Convert.ToBoolean(sdr["Ativo"].ToString())
                        };
                        categorias.Add(categoria);
                    }

                }

            }

            // devolver a lista preenchida
            return categorias;
        }

        /// <summary>
        /// Listar todas as categorias ativas do sistema
        /// </summary>
        /// <returns></returns>
        public List<CategoriaDomain> ListarAtivos()
        {

            List<CategoriaDomain> categorias = new List<CategoriaDomain>();

            // abrir uma conexao com o banco
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // fazer a leitura de todos os registros
                // declarar a instrucao a ser realizada
                string Query = "SELECT CategoriaId, Nome, DataCriacao, Ativo FROM Categorias WHERE Ativo = 1";

                // abre a conexao com o bd
                con.Open();
                // declaro para percorrer a lista
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // executa a query
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        CategoriaDomain categoria = new CategoriaDomain
                        {
                            CategoriaId = Convert.ToInt32(sdr["CategoriaId"]),
                            Nome = sdr["Nome"].ToString(),
                            DataCriacao = Convert.ToDateTime(sdr["DataCriacao"].ToString()),
                            Ativo = Convert.ToBoolean(sdr["Ativo"].ToString())
                        };
                        categorias.Add(categoria);
                    }

                }

            }

            // devolver a lista preenchida
            return categorias;
        }


        /// <summary>
        /// Cadastrar uma categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        public void Cadastrar(CategoriaDomain categoria)
        {
            // string correspondente no banco de dados para inserir uma categoria
            string Query = "INSERT INTO Categorias (Nome, DataCriacao, Ativo) VALUES (@Nome, @DataCriacao, @Ativo)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // declara o comando com a query e a conexao
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", categoria.Nome);
                cmd.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
                cmd.Parameters.AddWithValue("@Ativo", categoria.Ativo);
                // abre a conexao
                con.Open();
                // executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Buscar uma unica categoria por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Categoria encontrada</returns>
        public CategoriaDomain BuscarPorId(int id)
        {
            // query de consulta por id
            string Query = "SELECT CategoriaId, Nome, DataCriacao, Ativo FROM Categorias WHERE CategoriaId = @CategoriaId";

            // aonde, em qual local
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // abre a conexao
                con.Open();
                // guardar os dados
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    // adiciona o parametro
                    cmd.Parameters.AddWithValue("@CategoriaId", id);
                    sdr = cmd.ExecuteReader();

                    // caso encontre registros
                    if (sdr.HasRows)
                    {
                        // faz a leitura do registro encontrado
                        while (sdr.Read())
                        {
                            // devolve a categoria encontrada
                            CategoriaDomain categoria = new CategoriaDomain
                            {
                                CategoriaId = Convert.ToInt32(sdr["CategoriaId"]),
                                Nome = sdr["Nome"].ToString(),
                                DataCriacao = Convert.ToDateTime(sdr["DataCriacao"].ToString()),
                                Ativo = Convert.ToBoolean(sdr["Ativo"].ToString())
                            };
                            return categoria;
                        }
                    }
                    // caso nao encontre valor, devolve o registro nulo
                    return null;
                }
            }

        }

        /// <summary>
        /// Atualizar uma categoria
        /// </summary>
        /// <param name="categoriaDomain"></param>
        public void Atualizar(CategoriaDomain categoriaDomain)
        {
            // defino a query do sql
            string Query = "UPDATE Categorias SET Nome = @Nome, Ativo = @Ativo WHERE CategoriaId = @CategoriaId";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // faco o comando
                SqlCommand cmd = new SqlCommand(Query, con);
                // defino os parametros
                cmd.Parameters.AddWithValue("@Nome", categoriaDomain.Nome);
                cmd.Parameters.AddWithValue("@Ativo", categoriaDomain.Ativo);
                cmd.Parameters.AddWithValue("@CategoriaId", categoriaDomain.CategoriaId);
                // abrir a conexao
                con.Open();
                // executar
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Deletar uma categoria por id
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            string Query = "DELETE FROM Categorias WHERE CategoriaId = @CategoriaId";
            // conexao
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // comando
                SqlCommand cmd = new SqlCommand(Query, con);
                // defino os parametros
                cmd.Parameters.AddWithValue("@CategoriaId", id);
                con.Open();
                // executar
                cmd.ExecuteNonQuery();
            }
        }

    }
}
