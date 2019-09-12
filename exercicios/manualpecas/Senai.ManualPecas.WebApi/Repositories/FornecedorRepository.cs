using Microsoft.AspNetCore.Http;
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
    public class FornecedorRepository : IFornecedorRepository
    {
        public void Atualizar(Fornecedores fornecedor)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Fornecedores.Update(fornecedor);
                ctx.SaveChanges();
            }
        }
        public void Cadastrar(Fornecedores fornecedor)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                ctx.Fornecedores.Add(fornecedor);
                ctx.SaveChanges();
            }
        }

        public Fornecedores BuscarPorCNPJeSenha(LoginViewModel login)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                Fornecedores fornecedor = ctx.Fornecedores.FirstOrDefault(x => x.Cnpj == login.Cnpj && x.Senha == login.Senha);
                if (fornecedor == null)
                    return null;

                return fornecedor;
            }
        }
        public Fornecedores BuscarPorId(int fornecedorId)
        {
            using (ManualPecasContext ctx = new ManualPecasContext())
            {
                Fornecedores fornecedor = ctx.Fornecedores.FirstOrDefault(x => x.FornecedorId == fornecedorId);
                if (fornecedor == null)
                    return null;

                return fornecedor;
            }
        }

        public Pecas BuscarMaisBaratos(int pecaId)
        {
            string StringConexao = "Data Source = localhost; Initial Catalog = ManualPecas; Integrated Security = True";
            List<Fornecedores> fornecedores = new List<Fornecedores>();
            Pecas peca = new Pecas();
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "EXEC prListaMaisBarato @id";

                con.Open();

                SqlDataReader sdr;
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", pecaId);
                    sdr = cmd.ExecuteReader();
                    
                   List<FornecedoresPecas> listaFP = new                   List<FornecedoresPecas>();
                    while (sdr.Read())
                    {
                        peca.PecaId = Convert.ToInt32(sdr["PecaId"]);
                        peca.Descricao = sdr["Descricao"].ToString();
                        peca.Codigo = sdr["Codigo"].ToString();
                        FornecedoresPecas FP = new FornecedoresPecas()
                        {
                            Preco = (float)sdr["Preco"],
                            Peca = peca,
                            Fornecedor = new Fornecedores
                            {
                                FornecedorId = Convert.ToInt32(sdr["FornecedorId"]),
                                Cnpj = sdr["CNPJ"].ToString(),
                                Nome = sdr["Nome"].ToString(),
                                ListaFornecedoresPecas = listaFP
                            }
                            
                        };
                        listaFP.Add(FP);
                    }
                    peca.ListaFornecedoresPecas = listaFP;
                }
            }
            return peca;
        }
    }
}
