using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.CgStore.WebApi.Domains;
using Senai.CgStore.WebApi.Interfaces;
using Senai.CgStore.WebApi.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Senai.CgStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private ICategoriaRepository CategoriaRepository { get; set; }

        public CategoriasController()
        {
            CategoriaRepository = new CategoriaRepository();
        }

        /// <summary>
        /// Listar todas as categorias
        /// </summary>
        /// <returns>Lista de Categorias</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(CategoriaRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Categorias categoria = CategoriaRepository.BuscarPorId(id);
                if (categoria == null)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            
        }

        /// <summary>
        /// Cadastrar uma Categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns></returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Cadastrar(Categorias categoria)
        {
            try
            {
                int UsuarioId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                categoria.UsuarioId = UsuarioId;
                categoria.DataCriacao = DateTime.Now;
                CategoriaRepository.Cadastrar(categoria);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}