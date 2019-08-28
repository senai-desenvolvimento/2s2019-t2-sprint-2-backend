using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories
{
    public class ArtistaRepository
    {
        public List<Artistas> Listar()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Artistas.ToList();
            }
        }

        public void Cadastrar(Artistas artista)
        {
            using (OptusContext ctx = new OptusContext())
            {
                ctx.Artistas.Add(artista);
                ctx.SaveChanges();
            }
        }

        public int QuantidadeArtistas()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Artistas.Count();
            }
        }
    }
}
