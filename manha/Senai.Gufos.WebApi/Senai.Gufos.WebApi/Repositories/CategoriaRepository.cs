using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class CategoriaRepository
    {
        // nao ira mudar o acesso ao banco de dados, nao irei deixar de acessa-lo
        // o que ira mudar eh como eu faco esse acesso
        /// <summary>
        /// Listar todas as categorias.
        /// </summary>
        /// <returns>Lista de Categorias</returns>
        public List<Categorias> Listar()
        {
            using (GufosContext ctx = new GufosContext())
            {
                // SELECT * FROM Categorias
                return ctx.Categorias.ToList();
            }
        }

        public void Cadastrar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                // INSERT INTO
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
            }
        }

        // BuscarPorId
        public Categorias BuscarPorId(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                // select com where
                // id da nossa tabela seja igual ao id enviado pelo usuario
                return ctx.Categorias.FirstOrDefault(x => x.IdCategoria == id);
            }
        }

        // Atualizar
        public void Atualizar(Categorias categoria)
        {
            using (GufosContext ctx = new GufosContext())
            {
                // busco a categoria
                Categorias CategoriaBuscada = ctx.Categorias.FirstOrDefault(x => x.IdCategoria == categoria.IdCategoria);
                // SET
                CategoriaBuscada.Nome = categoria.Nome;
                // atualizo no contexto
                ctx.Categorias.Update(CategoriaBuscada);
                // efetivo no database
                ctx.SaveChanges();
            }
        }

        // Deletar
        public void Deletar(int id)
        {
            using (GufosContext ctx = new GufosContext())
            {
                // DELETE FROM Categorias WHERE IdCategoria = @Id;
                // encontrar quem eu quero deletar
                Categorias CategoriaBuscada = ctx.Categorias.Find(id);
                // remover o fofinho do contexto
                ctx.Categorias.Remove(CategoriaBuscada);
                // efetivar no banco essa mudança
                ctx.SaveChanges();
            }
        }

    }
}
