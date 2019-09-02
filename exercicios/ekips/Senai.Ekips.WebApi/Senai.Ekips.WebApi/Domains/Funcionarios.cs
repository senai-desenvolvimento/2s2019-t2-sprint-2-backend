using System;
using System.Collections.Generic;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class Funcionarios
    {
        public int FuncionarioId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public decimal? Salario { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CargoId { get; set; }
        public int? UsuarioId { get; set; }

        public Cargos Cargo { get; set; }
        public Departamentos Departamento { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
