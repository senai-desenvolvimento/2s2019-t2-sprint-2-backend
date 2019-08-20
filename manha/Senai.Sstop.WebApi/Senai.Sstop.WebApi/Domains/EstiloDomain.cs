using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Domains
{
    public class EstiloDomain
    {
        public int IdEstilo { get; set; }
        [Required(ErrorMessage = "O Nome do Estilo Musical é obrigatório.")]
        public string Nome { get; set; }
    }
}
