using System;
using System.Collections.Generic;

namespace Senai.CgStore.WebApi.Domains
{
    public partial class Categorias
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public int? UsuarioId { get; set; }

        public Usuarios Usuario { get; set; }
    }
}
