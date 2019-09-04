using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.ViewModels
{
    public class LancamentoViewModel
    {
        public string NomeJogo { get; set; }
        public DateTime DataLancamento { get; set; }
        public int QtdDias { get; set; }
    }
}
