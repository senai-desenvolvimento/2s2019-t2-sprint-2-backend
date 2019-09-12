using System;
using System.Collections.Generic;

namespace Senai.ManualPecas.WebApi.Domains
{
    public partial class Fornecedores
    {
        public int FornecedorId { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        public virtual List<FornecedoresPecas> ListaFornecedoresPecas { get; set; }
    }
}
