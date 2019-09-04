using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Interfaces;
using Senai.Gufos.WebApi.Repositories;

namespace Senai.Gufos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //CategoriaRepository CategoriaRepository = new CategoriaRepository();

        private ICategoriaRepository CategoriaRepository { get; set; }

        public CategoriasController()
        {
            CategoriaRepository = new CategoriaRepository();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(CategoriaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Categorias Categoria = CategoriaRepository.BuscarPorId(id);
            if (Categoria == null)
            {
                return NotFound();
            }
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
                Categorias CategoriaBuscada = CategoriaRepository.BuscarPorId(categoria.IdCategoria);
                if (CategoriaBuscada == null)
                    return NotFound();

                CategoriaRepository.Atualizar(categoria);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

}