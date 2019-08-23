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
    public class FilmesController : ControllerBase
    {
        // criar uma instancia do repositorio de filmes, para ter acesso a todas as funcoes
        FilmeRepository FilmeRepository = new FilmeRepository();

        /// <summary>
        /// Lista de Filmes
        /// </summary>
        /// <returns>Lista de Filmes</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(FilmeRepository.Listar());
        }

        /// <summary>
        /// Busca um determinado filme por id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Retorna o filme pesquisado.</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FilmeDomain filmeBuscado = FilmeRepository.BuscarPorId(id);
            if (filmeBuscado == null)
                return NotFound();
            return Ok(filmeBuscado);
        }

        /// <summary>
        /// Cadastrar um filme
        /// </summary>
        /// <param name="filme">FilmeDomain</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(FilmeDomain filme)
        {
            try
            {
                FilmeRepository.Cadastrar(filme);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro." + ex.Message });
            }
        }
    }
}