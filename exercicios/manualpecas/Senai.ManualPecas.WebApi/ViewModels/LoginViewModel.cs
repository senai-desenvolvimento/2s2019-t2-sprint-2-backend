using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Cnpj { get; set; }
        public string Senha { get; set; }
    }
}
