using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Sstop.WebApi.Domains;
using Senai.Sstop.WebApi.Repositories;

namespace Senai.Sstop.WebApi.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {
        // instanciar o repositorio
        ArtistaRepository ArtistaRepository = new ArtistaRepository();

        /// <summary>
        /// Listar todos os artistas.
        /// </summary>
        /// <returns>Lista de Artistas</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(ArtistaRepository.Listar());
        }


        [HttpPost]
        public IActionResult Cadastrar(ArtistaDomain artista)
        {

            //tenta fazer alguma coisa
            //    cadastrar um novo artista
            //caso ocorra algum erro
            //    retorna uma mensagem de erro

            try
            {
                ArtistaRepository.Cadastrar(artista);
                return Ok();
            }
            catch (Exception ex)
            {
                // nao foi realizada com sucesso.
                return BadRequest(new { mensagem = "Ocorreu um erro." + ex.Message });
            }
        }
    }
}