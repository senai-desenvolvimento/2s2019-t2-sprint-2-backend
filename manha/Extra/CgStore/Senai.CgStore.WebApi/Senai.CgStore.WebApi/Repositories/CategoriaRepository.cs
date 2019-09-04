using Senai.CgStore.WebApi.Domains;
using Senai.CgStore.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.CgStore.WebApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Categorias BuscarPorId(int id)
        {
            using (CgStoreContext ctx = new CgStoreContext())
            {
                return ctx.Categorias.Find(id);
            }
        }

        public void Cadastrar(Categorias categoria)
        {
            using (CgStoreContext ctx = new CgStoreContext())
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        public List<Categorias> Listar()
        {
            using (CgStoreContext ctx = new CgStoreContext())
            {
                return ctx.Categorias.ToList();
            }
        }
    }
}
