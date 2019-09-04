using Senai.CgStore.WebApi.Domains;
using System.Collections.Generic;

namespace Senai.CgStore.WebApi.Interfaces
{
    public interface ICategoriaRepository
    {
        
        List<Categorias> Listar();
        
        void Cadastrar(Categorias categoria);

        Categorias BuscarPorId(int id);

    }
}
