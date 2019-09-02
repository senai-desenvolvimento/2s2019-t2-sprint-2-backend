using Microsoft.EntityFrameworkCore;
using Senai.Ekips.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Ekips.WebApi.Repositories
{
    public class CargoRepository
    {
        public List<Cargos> Listar()
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.ToList();
            }
        }

        public Cargos ListarCargoComFuncionarios(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.Include(x => x.Funcionarios).FirstOrDefault(x => x.CargoId == id);
            }
        }

        public Cargos BuscarPorId(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.Find(id);
            }
        }

        public void Cadastrar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                ctx.Cargos.Add(cargo);
                ctx.SaveChanges();
            }
        }

        public void Atualizar(Cargos cargo)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                Cargos cargoBuscado = ctx.Cargos.Find(cargo.CargoId);
                cargoBuscado.Nome = cargo.Nome;
                cargoBuscado.Ativo = cargo.Ativo;
                ctx.Cargos.Update(cargoBuscado);
                ctx.SaveChanges();
            }
        }

        public Cargos ListarFuncionariosDoCargoPorNome(string nome)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.Include(x => x.Funcionarios).FirstOrDefault(x => x.Nome == nome);
            }
        }

        public List<Cargos> ListarCargosAtivos()
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                return ctx.Cargos.Where(x => x.Ativo == true).ToList();
            }
        }

        /* public void Deletar(int id)
        {
            using (EkipsContext ctx = new EkipsContext())
            {
                ctx.Cargos.Remove(ctx.Cargos.Find(id));
                ctx.SaveChanges();
            }
        } */
    }
}
