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
    public class GenerosController : ControllerBase
    {
        GeneroRepository GeneroRepository = new GeneroRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            List<GeneroDomain> generos = GeneroRepository.Listar();
            if (generos.Count() == 0)
                return NoContent();
            return Ok(generos);
        }

        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain genero)
        {
            GeneroRepository.Cadastrar(genero);
            return Ok();
        }

        [HttpGet("buscar/{nome}/livros")]
        public IActionResult BuscarLivroPorGenero(string nome)
        {
            GeneroDomain genero = GeneroRepository.BuscarLivroPorGenero(nome);
            if (genero == null)
                return NoContent();
            return Ok(genero);
        }
    }
}