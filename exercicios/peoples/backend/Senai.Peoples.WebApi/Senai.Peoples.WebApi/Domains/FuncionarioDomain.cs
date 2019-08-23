using System;
using System.ComponentModel.DataAnnotations;

namespace Senai.Peoples.WebApi.Domains
{
    // classe que representa o funcionario com suas caracteristicas. QUando comecamos primeiro com a estrutura do banco, elas sao similares as entidades e tendem a ter as mesmas caracteristicas (propriedades)
    public class FuncionarioDomain
    {
        // chave primaria - referente ao bd
        public int IdFuncionario { get; set; }
        [Required(ErrorMessage = "O Nome do Funcionário é obrigatório.")]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        // adicionar a propriedade que foi alterada no banco
        public DateTime DataNascimento { get; set; }
    }
}
