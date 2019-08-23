using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {

        FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        /// <summary>
        /// Listar todos os funcionários
        /// </summary>
        /// <returns>Lista de Funcionários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(funcionarioRepository.Listar());
        }

        /// <summary>
        /// Buscar um funcionário por id
        /// </summary>
        /// <param name="id">Id do Funcionário a ser pesquisado</param>
        /// <returns>Retorna o funcionario buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FuncionarioDomain funcionarioDomain = funcionarioRepository.BuscarPorId(id);
            if (funcionarioDomain == null)
                return NotFound();
            return Ok(funcionarioDomain);
        }

        /// <summary>
        /// Cadastrar um novo usuários.
        /// </summary>
        /// <param name="funcionarioDomain">Funcionario</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(FuncionarioDomain funcionarioDomain)
        {
            funcionarioRepository.Cadastrar(funcionarioDomain);
            return Ok();
        }

        /// <summary>
        /// Atualizar um funcionário
        /// </summary>
        /// <param name="id">Id do Funcionário</param>
        /// <param name="funcionario">Dados do Funcionário</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, FuncionarioDomain funcionario)
        {
            funcionario.IdFuncionario = id;
            funcionarioRepository.Alterar(funcionario);
            return Ok();
        }

        /// <summary>
        /// Deletar um funcionário
        /// </summary>
        /// <param name="id">Id do Funcionário a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            funcionarioRepository.Deletar(id);
            return Ok();
        }

        /// <summary>
        /// Buscar uma determinada lista de funcionarios a partir do nome a ser pesquisado.
        /// </summary>
        /// <param name="nome">Nome a ser pesquisado.</param>
        /// <returns>Lista de Funcionários</returns>
        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                List<FuncionarioDomain> funcionarios = funcionarioRepository.BuscarPorNome(nome);
                // caso nenhum funcionario seja encontrado com aquele nome, retorno sem conteudo
                if (funcionarios.Count == 0)
                    return NoContent();
                // caso contrario, apresento a lista de funcionarios com determinado nome
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro." + ex.Message });
            }
        }

        /// <summary>
        /// Ordenar os funcionários por nome.
        /// </summary>
        /// <param name="ordem">Ordem esperada. ASC ou DESC.</param>
        /// <returns>Retorna a lista de funcionários de maneira ordenada.</returns>
        [HttpGet("ordenacao/{ordem}")]
        public IActionResult ListarOrdenacao(string ordem)
        {
            if (ordem.ToUpper() == "ASC" || ordem.ToUpper() == "DESC")
                return Ok(funcionarioRepository.ListarPorOrdem(ordem));
            else
                return BadRequest(new { mensagem = "Ocorreu um erro. Identifique se é ASC ou DESC." });

        }
    }
}