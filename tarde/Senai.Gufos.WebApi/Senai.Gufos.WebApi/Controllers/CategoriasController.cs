using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;

namespace Senai.Gufos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        CategoriaRepository CategoriaRepository = new CategoriaRepository();

        [Authorize]
        [HttpGet]
        // IEnumerable<Categorias>
        public IActionResult Listar()
        {
            return Ok(CategoriaRepository.Listar());
        }

        /// <summary>
        /// Cadastrar uma categoria.
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpPost]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Eita, erro: " + ex.Message });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Categorias Categoria = CategoriaRepository.BuscarPorId(id);
            if (Categoria == null)
                return NotFound();
            return Ok(Categoria);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            CategoriaRepository.Deletar(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(Categorias categoria)
        {
            try 
            {
                // pesquisar uma categoria
                Categorias CategoriaBuscada = CategoriaRepository.BuscarPorId(categoria.IdCategoria);
                // caso nao encontre, not found
                if (CategoriaBuscada == null)
                    return NotFound();
                // caso contrario, se ela for encontrada, eu atualizo pq quero
                CategoriaRepository.Atualizar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ah, não. By - Pedro." });
            }
        }
    }
}