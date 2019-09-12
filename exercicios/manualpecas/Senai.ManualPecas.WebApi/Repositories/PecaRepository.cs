using Microsoft.EntityFrameworkCore;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Repositories
{
    public class PecaRepository : IPecasRepository
    {
        public void Atualizar(Pecas peca)
        {
            using(ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Pecas.Update(peca);
                ctx.SaveChanges();
            }
        }
        public void Cadastrar(PecaViewModel peca)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                var Codigo = new SqlParameter("Codigo", peca.Codigo);
                var Descricao = new SqlParameter("Descricao", peca.Descricao);
                var Preco = new SqlParameter("Preco", peca.Preco);
                var FornecedorId = new SqlParameter("FornecedorId", peca.FornecedorId);

                ctx.Database.ExecuteSqlCommand("EXEC dbo.prAdicionaERetornaPeca @Codigo, @Descricao, @FornecedorId, @Preco",
                    Codigo, Descricao, FornecedorId, Preco);
            }
        }
        public void Deletar(PecaViewModel peca)
        {
            using(ManualPecasContext ctx = new ManualPecasContext())
            {
                var pecaId = new SqlParameter("PecaId", peca.PecaId);
                var fornecedorId = new SqlParameter("ForncedorId", peca.FornecedorId);

                ctx.Database.ExecuteSqlCommand("EXEC prRemovePeca @PecaId, @ForncedorId",
                    pecaId, fornecedorId);
            }
        }

        public Fornecedores BuscarPorFornecedor(int fornecedorId)
        {
            string StringConexao = "Data Source = localhost; Initial Catalog = ManualPecas; Integrated Security = True";

            Fornecedores fornecedor = new Fornecedores();
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM vwJoinFornecedoresPecas WHERE FornecedorId = @id ORDER BY Codigo ASC";

                con.Open();

                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", fornecedorId);
                    sdr = cmd.ExecuteReader();
                    List<FornecedoresPecas> listaFP = new List<FornecedoresPecas>();
                    while (sdr.Read())
                    {
                        fornecedor.FornecedorId = Convert.ToInt32(sdr["FornecedorId"]);
                        fornecedor.Cnpj = sdr["CNPJ"].ToString();
                        fornecedor.Nome = sdr["Nome"].ToString();

                        FornecedoresPecas FP = new FornecedoresPecas()
                        {
                            Preco = (float)sdr["Preco"],
                            Peca = new Pecas
                            {
                                PecaId = Convert.ToInt32(sdr["PecaId"]),
                                Descricao = sdr["Descricao"].ToString(),
                                Codigo = sdr["Codigo"].ToString(),
                                ListaFornecedoresPecas = listaFP

                            }

                        };
                        listaFP.Add(FP);
                    }
                    fornecedor.ListaFornecedoresPecas = listaFP;
                }
            }
            return fornecedor;
        }

        public List<Pecas> Listar()
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                return ctx.Pecas.ToList();
            }
        }
        public List<Pecas> ListarEmOrdemCrescente()
        {
            string StringConexao = "Data Source = localhost; Initial Catalog = ManualPecas; Integrated Security = True";
            List<Pecas> pecas = new List<Pecas>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT * FROM vwJoinFornecedoresPecas ORDER BY Preco ASC";

                con.Open();

                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Pecas peca = new Pecas
                        {
                            PecaId = Convert.ToInt32(sdr["PecaId"]),
                            Descricao = sdr["Descricao"].ToString(),
                            Codigo = sdr["Codigo"].ToString(),
                            ListaFornecedoresPecas = new List<FornecedoresPecas>
                            {
                                new FornecedoresPecas
                                {
                                    Preco = (float) sdr["Preco"],
                                    Fornecedor = new Fornecedores
                                    {
                                        FornecedorId = Convert.ToInt32(sdr["FornecedorId"]),
                                        Cnpj = sdr["CNPJ"].ToString(),
                                        Nome = sdr["Nome"].ToString()
                                    }
                                }
                            }
                        };
                        pecas.Add(peca);
                    }
                }
            }
            return pecas;
        }
        public Pecas BuscarPorId(int pecaId)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                return ctx.Pecas.FirstOrDefault(x => x.PecaId == pecaId);
            }
        }
    }
}
