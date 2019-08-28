using Microsoft.EntityFrameworkCore;
using Senai.Optus.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Optus.WebApi.Repositories
{
    public class EstiloRepository
    {
        public List<Estilos> Listar()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.ToList();
            }
        }

        public void Cadastrar(Estilos estilo)
        {
            using (OptusContext ctx = new OptusContext())
            {
                ctx.Estilos.Add(estilo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Estilos estilo)
        {
            using (OptusContext ctx = new OptusContext())
            {
                Estilos EstiloBuscado = ctx.Estilos.Find(estilo.IdEstilo);
                EstiloBuscado.Nome = estilo.Nome;
                ctx.Estilos.Update(EstiloBuscado);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (OptusContext ctx = new OptusContext())
            {
                Estilos EstiloBuscado = BuscarPorId(id);
                ctx.Estilos.Remove(EstiloBuscado);
                ctx.SaveChanges();
            }
        }

        public Estilos BuscarPorId(int id)
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.Find(id);
            }
        }

        public Estilos ListarArtistasPorIdEstilo(int id)
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.Include(x => x.Artistas).FirstOrDefault(x => x.IdEstilo == id);
            }
        }

        public Estilos ListarArtistasPorNomeEstilo(string nome)
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.Include(x => x.Artistas).FirstOrDefault(x => x.Nome == nome);
            }
        }

        public int QuantidadeEstilos()
        {
            using (OptusContext ctx = new OptusContext())
            {
                return ctx.Estilos.Count();
            }
        }
    }
}
