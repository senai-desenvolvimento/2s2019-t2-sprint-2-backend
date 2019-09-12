using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.Repositories;
using Senai.ManualPecas.WebApi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Senai.ManualPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private IPecasRepository PecasRepository { get; set; }

        public PecasController()
        {
            PecasRepository = new PecaRepository();
        }

        [HttpPut("{pecaId}")]
        [Authorize]
        public IActionResult Atualizar(int pecaId, PecaViewModel peca)
        {
            try
            {
                Pecas pecaBuscada = PecasRepository.BuscarPorId(pecaId);

                if (pecaBuscada == null)
                    return NoContent();

                pecaBuscada.Codigo = peca.Codigo ?? pecaBuscada.Codigo;
                pecaBuscada.Descricao = peca.Descricao ?? pecaBuscada.Descricao;

                PecasRepository.Atualizar(pecaBuscada);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Cadastrar(PecaViewModel peca)
        {
            peca.FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            PecasRepository.Cadastrar(peca);
            return Ok();
        }

        [HttpDelete("{pecaId}")]
        [Authorize]
        public IActionResult Deletar(int pecaId)
        {
            PecaViewModel peca = new PecaViewModel
            {
                PecaId = pecaId,
                FornecedorId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value)
            };

            PecasRepository.Deletar(peca);
            return Ok();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(PecasRepository.Listar());
        }

        [HttpGet("ListarCrescente")]
        public IActionResult ListarEmOrdemCrescente()
        {
            return Ok(PecasRepository.ListarEmOrdemCrescente());
        }

        [HttpGet("BuscarPorFornecedor/{fornecedorId}")]
        public IActionResult BuscarPorFornecedor(int fornecedorId)
        {
            return Ok(PecasRepository.BuscarPorFornecedor(fornecedorId));
        }
    }
}