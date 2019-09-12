using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Interfaces
{
    interface IFornecedorRepository
    {
        void Cadastrar(Fornecedores fornecedor);
        void Atualizar(Fornecedores fornecedor);
        Fornecedores BuscarPorCNPJeSenha(LoginViewModel login);
        Fornecedores BuscarPorId(int fornecedorId);
        Pecas BuscarMaisBaratos(int pecaId);
    }
}
