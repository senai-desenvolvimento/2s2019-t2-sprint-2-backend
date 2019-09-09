using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.ViewModels
{
    public class PecasPrecosViewModel
    {
        public int PecaId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public float Peso { get; set; }
        public float PrecoCusto { get; set; }
        public float PrecoVenda { get; set; }
        public float PrecoGanho { get; set; }
        public float Porcentagem { get; set; }
    }
}
