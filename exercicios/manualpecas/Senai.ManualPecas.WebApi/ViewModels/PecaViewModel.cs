using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.ViewModels
{
    public class PecaViewModel
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public float Preco { get; set; }

        public int FornecedorId { get; set; }
        public int PecaId { get; set; }
    }
}
