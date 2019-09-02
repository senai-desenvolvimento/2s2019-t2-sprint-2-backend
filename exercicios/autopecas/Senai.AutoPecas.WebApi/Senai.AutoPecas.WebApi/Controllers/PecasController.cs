using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.AutoPecas.WebApi.Domains;
using Senai.AutoPecas.WebApi.Repositories;

namespace Senai.AutoPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class PecasController : ControllerBase
    {
        PecaRepository PecaRepository = new PecaRepository();
        
        [HttpGet]
        public IActionResult Listar()
        {
            int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
            return Ok(PecaRepository.Listar(FornecedorId));
        }

        [HttpPost]
        public IActionResult Cadastrar(Pecas peca)
        {
            try
            {
                int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
                peca.FornecedorId = FornecedorId;
                PecaRepository.Cadastrar(peca);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
                Pecas pecaBuscada = PecaRepository.BuscarPorId(id, FornecedorId);
                if (pecaBuscada == null)
                    return NotFound();
                return Ok(pecaBuscada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Pecas peca)
        {
            try
            {
                int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
                Pecas pecaBuscada = PecaRepository.BuscarPorId(id, FornecedorId);
                if (pecaBuscada == null)
                    return NotFound();
                peca.FornecedorId = FornecedorId;
                peca.PecaId = id;
                PecaRepository.Atualizar(peca);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
                Pecas pecaBuscada = PecaRepository.BuscarPorId(id, FornecedorId);
                if (pecaBuscada == null)
                    return NotFound();
                PecaRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
        
        [HttpGet("precos")]
        public IActionResult ListarPrecos()
        {
            int FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == "FornecedorId").Value);
            return Ok(PecaRepository.ListarPrecos(FornecedorId));
        }
    }
}