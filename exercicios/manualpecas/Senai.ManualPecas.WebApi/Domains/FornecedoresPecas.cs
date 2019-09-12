using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Domains
{
    public class FornecedoresPecas
    {
        public virtual Fornecedores Fornecedor { get; set; }
        public int? FornecedorId { get; set; }

        public int? PecaId { get; set; }
        public virtual Pecas Peca { get; set; }

        public float Preco { get; set; }

    }
}
