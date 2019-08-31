using System;

namespace Senai.SqlClient.WebApi.Domains
{
    // Criar o dominio com referencia as colunas do banco de dados
    public class CategoriaDomain
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
