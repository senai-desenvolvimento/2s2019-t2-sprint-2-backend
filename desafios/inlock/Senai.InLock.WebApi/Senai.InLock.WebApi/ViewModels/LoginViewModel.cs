using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A senha é obrigatória e deve conter entre 5 e 30 caractereces.")]
        public string Senha { get; set; }
    }
}
