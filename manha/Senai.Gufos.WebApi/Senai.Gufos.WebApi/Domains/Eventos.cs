using System;
using System.Collections.Generic;

namespace Senai.Gufos.WebApi.Domains
{
    public partial class Eventos
    {
        public int IdEvento { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEvento { get; set; }
        public string Localizacao { get; set; }
        public bool? Ativo { get; set; }
        // utilizado para cadastrar
        public int? IdCategoria { get; set; }
        // utilizado para listar
        public Categorias IdCategoriaNavigation { get; set; }
    }
}
