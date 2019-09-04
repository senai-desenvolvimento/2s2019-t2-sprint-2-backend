using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class JogoRepository
    {
        public List<Jogos> Listar()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.ToList();
            }
        }

        public Jogos BuscarPorId(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.Find(id);
            }
        }

        public List<Jogos> BuscarPorNome(string nome)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Jogos.Where(x => x.NomeJogo.Contains(nome)).ToList();
            }
        }

        public void Cadastrar(Jogos estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                ctx.Jogos.Add(estudio);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Jogos jogo = ctx.Jogos.Find(id);
                ctx.Jogos.Remove(jogo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(int id, Jogos jogo)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Jogos jogoBuscado = ctx.Jogos.Find(id);
                jogoBuscado.DataLancamento = jogo.DataLancamento;
                jogoBuscado.Descricao = jogo.Descricao;
                jogoBuscado.EstudioId = jogo.EstudioId;
                jogoBuscado.NomeJogo = jogo.NomeJogo;
                jogoBuscado.Valor = jogo.Valor;
                ctx.Jogos.Update(jogoBuscado);
                ctx.SaveChanges();
            }
        }
    }
}
