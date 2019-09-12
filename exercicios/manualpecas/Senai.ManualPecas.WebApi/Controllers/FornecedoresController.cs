using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Interfaces;
using Senai.ManualPecas.WebApi.Repositories;

namespace Senai.ManualPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private IFornecedorRepository FornecedorRepository { get; set; }

        public FornecedoresController()
        {
            FornecedorRepository = new FornecedorRepository();
        }

        [HttpPut]
        [Authorize]
        public IActionResult Atualizar(Fornecedores fornecedor)
        {
            try
            {
                int tokenId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

                Fornecedores fornecedorBuscado = FornecedorRepository.BuscarPorId(tokenId);

                fornecedorBuscado.Cnpj = fornecedor.Cnpj ?? fornecedorBuscado.Cnpj;
                fornecedorBuscado.Nome = fornecedor.Nome ?? fornecedorBuscado.Nome;
                fornecedorBuscado.Senha = fornecedor.Senha ?? fornecedorBuscado.Senha;

                FornecedorRepository.Atualizar(fornecedorBuscado);
                return Ok();
            }catch (Exception e)
            {
                return BadRequest(new { mensagem = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Fornecedores fornecedor)
        {
            try
            {
                FornecedorRepository.Cadastrar(fornecedor);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = e.Message });
            }
        }

        [HttpGet("MenorPreco/{pecaId}")]
        public IActionResult ListarMaisBaratos(int pecaId)
        {
            return Ok(FornecedorRepository.BuscarMaisBaratos(pecaId));
        }

    }
}