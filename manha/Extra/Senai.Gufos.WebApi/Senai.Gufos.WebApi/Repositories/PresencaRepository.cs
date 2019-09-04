using Microsoft.EntityFrameworkCore;
using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Repositories
{
    public class PresencaRepository
    {
        public List<Presencas> Listar()
        {
            using (GufosContext ctx = new GufosContext())
            {
                return ctx.Presencas.Include(x => x.Evento).Include(x => x.Usuario).ToList();
            }
        }
    }
}
