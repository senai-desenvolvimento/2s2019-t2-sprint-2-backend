using System.ComponentModel.DataAnnotations;

namespace Senai.Peoples.WebApi.Domains
{
    public class FuncionarioDomain
    {
        public int IdFuncionario { get; set; }
        [Required(ErrorMessage = "O Nome do Funcionário é obrigatório.")]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
