using Senai.Gufos.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufos.WebApi.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categorias> Listar();

        Categorias BuscarPorId(int id);

        void Cadastrar(Categorias categoria);

        void Atualizar(Categorias categoria);

        void Deletar(int id);
    }
}
