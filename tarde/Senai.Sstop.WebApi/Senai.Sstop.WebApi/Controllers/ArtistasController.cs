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

        ArtistaRepository ArtistaRepository = new ArtistaRepository();

        // /api/artistas -> retornar a lista de artistas
        [HttpGet]
        public IActionResult Listar()
        {
            //  chama ele.o que eu quero
            return Ok(ArtistaRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(ArtistaDomain artista)
        {
            try
            {
                // tenta fazer alguma coisa
                ArtistaRepository.Cadastrar(artista);
                // 200
                return Ok();
                // notfound - 404
            }
            catch (Exception ex)
            {
                // plano b
                // 400
                return BadRequest(new { mensagem = "Oi, fofo, deu ruim. Desculpa. Perdoa eu. Por favorzinho. Nunca te pedi nada." + ex.Message });
            }

        }


        [HttpPut]
        public IActionResult Atualizar()
        {
            return Ok();
        }

    }
}