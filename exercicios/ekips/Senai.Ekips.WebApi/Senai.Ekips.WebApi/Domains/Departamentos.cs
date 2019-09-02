using System;
using System.Collections.Generic;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class Departamentos
    {
        public Departamentos()
        {
            Funcionarios = new HashSet<Funcionarios>();
        }

        public int DepartamentoId { get; set; }
        public string Nome { get; set; }

        public ICollection<Funcionarios> Funcionarios { get; set; }
    }
}
