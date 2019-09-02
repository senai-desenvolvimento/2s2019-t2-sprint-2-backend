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
    [ApiController]
    public class LivrosController : ControllerBase
    {
        LivroRepository LivroRepository = new LivroRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            List<LivroDomain> livros = LivroRepository.Listar();
            if (livros.Count() == 0)
                return NoContent();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(LivroRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(LivroDomain livro)
        {
            LivroRepository.Cadastrar(livro);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, LivroDomain livro)
        {
            LivroDomain livroBuscado = LivroRepository.BuscarPorId(id);

            if (livroBuscado == null)
                return NotFound();

            livro.IdLivro = id;
            LivroRepository.Atualizar(livro);
            return Ok(LivroRepository.BuscarPorId(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            LivroDomain livro = LivroRepository.BuscarPorId(id);
            if (livro == null)
                return NotFound(new { mensagem = "Não encontrado" });

            LivroRepository.Deletar(id);
            return Ok();
        }
    }
}