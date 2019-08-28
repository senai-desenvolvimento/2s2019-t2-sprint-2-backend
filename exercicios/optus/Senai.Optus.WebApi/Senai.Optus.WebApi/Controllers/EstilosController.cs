using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Domains;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EstilosController : ControllerBase
    {
        EstiloRepository EstiloRepository = new EstiloRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(EstiloRepository.Listar());
        }

        [HttpPost]
        public IActionResult Cadastrar(Estilos estilo)
        {
            try
            {
                EstiloRepository.Cadastrar(estilo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Estilos estilo)
        {
            try
            {
                estilo.IdEstilo = id;
                if (EstiloRepository.BuscarPorId(id) == null)
                    return NotFound();
                EstiloRepository.Atualizar(estilo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (EstiloRepository.BuscarPorId(id) == null)
                    return NotFound();
                EstiloRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Estilos Estilo = EstiloRepository.BuscarPorId(id);
                if (Estilo == null)
                    return NotFound();
                return Ok(Estilo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpGet("{id}/artistas")]
        public IActionResult ListarArtistasPorIdEstilo(int id)
        {
            try
            {
                Estilos Estilo = EstiloRepository.ListarArtistasPorIdEstilo(id);
                if (Estilo == null)
                    return NotFound();
                return Ok(Estilo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpGet("buscar/{nome}/artistas")]
        public IActionResult ListarArtistasPorNomeEstilo(string nome)
        {
            try
            {
                Estilos Estilo = EstiloRepository.ListarArtistasPorNomeEstilo(nome);
                if (Estilo == null)
                    return NotFound();
                return Ok(Estilo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ocorreu um erro. " + ex.Message });
            }
        }

        [HttpGet("quantidade")]
        public IActionResult QuantidadeEstilos()
        {
            return Ok(new { quantidadeEstilos = EstiloRepository.QuantidadeEstilos() });
        }



    }
}