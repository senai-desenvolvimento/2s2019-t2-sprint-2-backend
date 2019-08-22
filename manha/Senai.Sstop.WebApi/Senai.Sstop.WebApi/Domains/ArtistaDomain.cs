using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Sstop.WebApi.Domains
{
    public class ArtistaDomain
    {

        public int IdArtista { get; set; }
        public string Nome { get; set; }
        // eu vou utilizar esse fofinho só para cadastrar
        public int EstiloId { get; set; }
        // nao preciso do estilo inteiro para realizar um novo cadastro
        public EstiloDomain Estilo { get; set; }

    }
}
