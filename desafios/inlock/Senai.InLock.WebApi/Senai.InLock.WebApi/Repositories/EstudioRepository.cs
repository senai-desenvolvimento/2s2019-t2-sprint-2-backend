using Microsoft.EntityFrameworkCore;
using Senai.InLock.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.Repositories
{
    public class EstudioRepository
    {
        public List<Estudios> Listar()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.ToList();
            }
        }

        public List<Estudios> ListarEstudiosComJogos()
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Include(x => x.Jogos).ToList();
            }
        }

        public List<Estudios> BuscarPorNome(string nome)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Include(x => x.Jogos).Where(x => x.NomeEstudio.Contains(nome)).ToList();
            }
        }

        public List<Estudios> BuscarMeusEstudios(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Where(x => x.UsuarioId == id).ToList();
            }
        }

        public List<Estudios> ListarEstudiosComUsuarios()
        {
            using (InLockContext ctx = new InLockContext())
            {
                var resultado = ctx.Estudios.Include(x => x.Usuario).ToList();
                resultado.ForEach(x => x.Usuario.Senha = null);
                return resultado;
            }
        }

        public List<Estudios> BuscarPorPais(string nome)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Include(x => x.Jogos).Where(x => x.PaisOrigem.Contains(nome)).ToList();
            }
        }

        public Estudios BuscarPorId(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                return ctx.Estudios.Find(id);
            }
        }

        public void Cadastrar(Estudios estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                ctx.Estudios.Add(estudio);
                ctx.SaveChanges();
            }
        }

        public void Deletar(int id)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Estudios estudio = ctx.Estudios.Find(id);
                ctx.Estudios.Remove(estudio);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(int id, Estudios estudio)
        {
            using (InLockContext ctx = new InLockContext())
            {
                Estudios estudioBuscado = ctx.Estudios.Find(id);
                estudioBuscado.NomeEstudio = estudio.NomeEstudio;
                ctx.Estudios.Update(estudioBuscado);
                ctx.SaveChanges();
            }
        }
    }
}
