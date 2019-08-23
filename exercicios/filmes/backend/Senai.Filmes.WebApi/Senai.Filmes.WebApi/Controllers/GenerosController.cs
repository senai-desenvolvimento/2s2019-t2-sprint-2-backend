using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Filmes.WebApi.Domains;
using Senai.Filmes.WebApi.Repositories;

namespace Senai.Filmes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GenerosController : ControllerBase
    {
        // criar uma instancia do repositorio de generos, para ter acesso a todas as funcoes
        GeneroRepository GeneroRepository = new GeneroRepository();

        /// <summary>
        /// Listar todos os generos.
        /// </summary>
        /// <returns>Retorna a lista de generos.</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(GeneroRepository.Listar());
        }

        /// <summary>
        /// Buscar um determinado genero por id.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Retornar uma lista de generos.</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            GeneroDomain generoBuscado = GeneroRepository.BuscarPorId(id);
            if (generoBuscado == null)
                return NotFound();
            return Ok(generoBuscado);
        }

        /// <summary>
        /// Cadastrar um genero.
        /// </summary>
        /// <param name="genero">GeneroDomain</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            try
            {
                GeneroRepository.Cadastrar(genero);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro." + ex.Message });
            }
        }

        /// <summary>
        /// Atualizar um genero
        /// </summary>
        /// <param name="genero">GeneroDomain</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(GeneroDomain genero, int id)
        {
            try
            {
                genero.IdGenero = id;
                GeneroRepository.Alterar(genero);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro." + ex.Message });
            }
        }

        /// <summary>
        /// Deletar um genero dado um determinado id.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                GeneroRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro." + ex.Message });
            }
        }

        /// <summary>
        /// Listar todos os filmes de um determinado genero
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Retorna o gênero e seus determinados filmes</returns>
        [HttpGet("{id}/filmes")]
        public IActionResult ListarFilmesDoGenero(int id)
        {
            GeneroDomain generoBuscado = GeneroRepository.BuscarPorId(id);
            if (generoBuscado == null)
                return NotFound(new { mensagem = "O gênero pesquisado não foi encontrado." });
            else
            {
                GeneroDomain genero = GeneroRepository.ListarFilmesDoGenero(id);
                if (genero.Filmes == null)
                    return NoContent();
                return Ok(genero);
            }
        }
    }
}