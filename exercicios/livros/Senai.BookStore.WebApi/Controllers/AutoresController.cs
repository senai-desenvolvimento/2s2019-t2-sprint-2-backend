using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.BookStore.WebApi.Domains;
using Senai.BookStore.WebApi.Repositories;

namespace Senai.BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        AutorRepository AutorRepository = new AutorRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            List<AutorDomain> autores = AutorRepository.Listar();
            if (autores.Count == 0)
                return NoContent();

            return Ok(autores);
        }

        [HttpPost]
        public IActionResult Cadastro(AutorDomain autor)
        {                 
            AutorRepository.Cadastrar(autor);
            return Ok();
        }

        [HttpGet("{id}/livros")]
        public IActionResult BuscarLivrosPorAutor(int id)
        {
            AutorDomain autor = AutorRepository.BuscarLivrosPorAutor(id);
            return Ok(autor);
        }

        [HttpGet("{id}/ativos/livros")]
        public IActionResult BuscarLivrosPorAutorAtivo(int id)
        {
            AutorDomain autor = AutorRepository.BuscarLivrosPorAutorAtivo(id);
            if (autor == null)
                return NoContent();

            return Ok(autor);
        }

        [HttpGet("ativos")]
        public IActionResult BuscarAutoresAtivos()
        {
            List<AutorDomain> autores = AutorRepository.BuscarAutoresAtivos();
            if (autores.Count == 0)
                return NoContent();

            return Ok(autores);
        }

        [HttpGet("{ano}/nascimento")]
        public IActionResult BuscarPorAnoDeNascimento(int ano)
        {
            // Recupera a data atual do servidor
            DateTime hoje = DateTime.Today;

            if (ano >= hoje.Year)
                return BadRequest(new { mensagens = "O ano deve ser inferior ao ano atual" });

            List<AutorDomain> autores = AutorRepository.BuscarPorAnoDeNascimento(ano);
            if (autores.Count == 0)
                return NoContent();
            else
                return Ok(autores);            
        }
    }
}