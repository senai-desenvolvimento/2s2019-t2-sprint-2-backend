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

        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns>200 com a lista de categorias</returns>
        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(CategoriaRepository.Listar());
        }
        /// <summary>
        /// Cadastrar uma categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
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
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        /// <summary>
        /// Buscar uma categoria por id.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Categoria Encontrada.</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Categorias Categoria = CategoriaRepository.BuscarPorId(id);
            if (Categoria == null)
                return NotFound();
            return Ok(Categoria);
        }

        /// <summary>
        /// Deletar uma categoria por id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Sucesso caso seja deletado.</returns>
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
                Categorias CategoriasBuscada = CategoriaRepository.BuscarPorId(categoria.IdCategoria);

                if (CategoriasBuscada == null)
                    return NotFound();

                CategoriaRepository.Atualizar(categoria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}