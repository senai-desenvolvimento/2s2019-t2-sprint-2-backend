using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.SqlClient.WebApi.Domains;
using Senai.SqlClient.WebApi.Repositories;

namespace Senai.SqlClient.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        // para ter acesso a todas as funcoes do repositorio de acesso, basta criar uma nova instancia do repositorio
        CategoriaRepository CategoriaRepository = new CategoriaRepository();

        // GET /api/categorias
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(CategoriaRepository.Listar());
        }

        // POST /api/categorias
        /// <summary>
        /// Cadastrar uma categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns></returns>
        [HttpPost]
        // public IActionResult Cadastrar([FromBody] CategoriaDomain categoria)
        // quando trabalhamos com apicontroller, nao precisamos informar o frombody
        public IActionResult Cadastrar(CategoriaDomain categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // GET /api/categorias/{id}
        /// <summary>
        /// Buscar uma categoria por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>A categoria encontrada</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                // verificar se a categoria a ser deletada, existe no banco de dados
                CategoriaDomain categoria = CategoriaRepository.BuscarPorId(id);
                // caso nao exista, retornar uma msg de categoria nao encontrada
                if (categoria == null)
                    return NotFound();
                // caso exista, retornar a categoria encontrada
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // DELETE /api/categorias/{id}
        /// <summary>
        /// Deletar uma categoria por id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                // verificar se a categoria a ser deletada, existe no banco de dados
                CategoriaDomain categoria = CategoriaRepository.BuscarPorId(id);
                // caso nao exista, retornar uma msg de categoria nao encontrada
                if (categoria == null)
                    return NotFound();
                // caso exista, deletar a categoria e retornar uma mensagem de sucesso
                CategoriaRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // PUT /api/categorias/{id}
        /// <summary>
        /// Atualizar uma categoria por id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="categoriaDomain">categoriaDomain</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, CategoriaDomain categoriaDomain)
        {
            try
            {
                // verificar se a categoria a ser deletada, existe no banco de dados
                CategoriaDomain categoria = CategoriaRepository.BuscarPorId(id);
                // caso nao exista, retornar uma msg de categoria nao encontrada
                if (categoria == null)
                    return NotFound();
                // caso exista, atualizar a categoria e retornar uma mensagem de sucesso
                categoriaDomain.CategoriaId = id;
                CategoriaRepository.Atualizar(categoriaDomain);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // GET /api/categorias/{nome}/buscar
        /// <summary>
        /// Buscar todas as categorias com um determinado nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("{nome}/buscar")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                // verificar se a categoria a ser deletada, existe no banco de dados
                List<CategoriaDomain> categorias = CategoriaRepository.BuscarPorNome(nome);
                // caso nao exista, retornar uma msg de categoria nao encontrada
                if (categorias.Count == 0)
                    return NotFound();
                // caso exista, retornar a categoria encontrada
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // GET /api/categorias/ativos/buscar
        /// <summary>
        /// Buscar todas as categorias ativas no banco de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("ativos/buscar")]
        public IActionResult ListarAtivos()
        {
            try
            {
                // verificar se a categoria a ser deletada, existe no banco de dados
                List<CategoriaDomain> categorias = CategoriaRepository.ListarAtivos();
                // caso nao exista, retornar uma msg de categoria nao encontrada
                if (categorias.Count == 0)
                    return NotFound();
                // caso exista, retornar a categoria encontrada
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}