using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Senai.Gufos.WebApi.Domains
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        //[StringLength(30, MinimumLength = 5, ErrorMessage = "A senha deve conter mais do que 5 caracteres.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A permissão é obrigatória.")]
        public string Permissao { get; set; }
    }
}
