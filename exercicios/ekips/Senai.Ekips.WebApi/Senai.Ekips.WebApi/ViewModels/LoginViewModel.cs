using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é requerido.")]
        public string Email { get; set; }
        [StringLength(200, MinimumLength = 4, ErrorMessage = "A senha é requerida e deve conter no mínimo 4 caracteres.")]
        public string Senha { get; set; }
    }
}
