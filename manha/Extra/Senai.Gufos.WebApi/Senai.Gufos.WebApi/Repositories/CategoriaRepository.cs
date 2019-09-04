using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public List<Categorias> Listar()
        {
            using (GufosContext ctx = new GufosContext())
            {
                return ctx.Categorias.ToList();
            }
        }

        public void Cadastrar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        public Categorias BuscarPorId(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                return ctx.Categorias.FirstOrDefault(x => x.IdCategoria == id);
            }
        }

        public void Deletar(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Categorias CategoriaBuscada = ctx.Categorias.Find(id);
                ctx.Categorias.Remove(CategoriaBuscada);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                Categorias CategoriaAtualizada = ctx.Categorias.FirstOrDefault(x => x.IdCategoria == categoria.IdCategoria);
                CategoriaAtualizada.Nome = categoria.Nome;
                ctx.Categorias.Update(CategoriaAtualizada);
                ctx.SaveChanges();
            }
        }
    }
}
