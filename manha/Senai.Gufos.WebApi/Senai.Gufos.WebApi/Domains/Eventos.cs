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
        public bool? Ativo { get; set; }
        public string Localizacao { get; set; }
        // cadastrar
        public int? IdCategoria { get; set; }
        // listar - mostrar os dados da categoria - join
        public Categorias IdCategoriaNavigation { get; set; }
    }
}
