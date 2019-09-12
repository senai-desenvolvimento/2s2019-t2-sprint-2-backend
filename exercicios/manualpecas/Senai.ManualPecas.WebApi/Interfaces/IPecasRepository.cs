using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Interfaces
{
    interface IPecasRepository
    {
        void Cadastrar(PecaViewModel peca);
        void Atualizar(Pecas peca);
        void Deletar(PecaViewModel peca);

        Fornecedores BuscarPorFornecedor(int fornecedorId);

        List<Pecas> Listar();
        List<Pecas> ListarEmOrdemCrescente();
        Pecas BuscarPorId(int pecaId);
    }
}
