using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Ekips.WebApi.Domains;
using Senai.Ekips.WebApi.Repositories;

namespace Senai.Ekips.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        FuncionarioRepository FuncionarioRepository = new FuncionarioRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {

            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //identity.Claims.First();

            string permissao = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            if (permissao == "ADMINISTRADOR")
                return Ok(FuncionarioRepository.Listar());
            else if (permissao == "COMUM")
                return Ok(FuncionarioRepository.BuscarFuncionarioPorUsuario(Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value)));
            else
                return Forbid();
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("dados")]
        public IActionResult ListarDados()
        {
            return Ok(FuncionarioRepository.ListarComDados());
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{salario}/salarios")]
        public IActionResult BuscarFuncionariosPorSalario(decimal salario)
        {

            return Ok(FuncionarioRepository.BuscarFuncionariosPorSalario(salario));
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet("{coluna}/{ordem}")]
        public IActionResult FuncionariosPorItemEOrdem(string coluna, string ordem)
        {
            try
            {
                if (ordem.ToUpper() != "ASC" && ordem.ToUpper() != "DESC")
                    return BadRequest();
                return Ok(FuncionarioRepository.BuscarPorItemEOrdem(coluna, ordem));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Cadastrar(Funcionarios funcionario)
        {
            try
            {
                FuncionarioRepository.Cadastrar(funcionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Funcionarios funcionario)
        {
            try
            {
                if (FuncionarioRepository.BuscarPorId(id) == null)
                    return NotFound();
                funcionario.FuncionarioId = id;
                FuncionarioRepository.Atualizar(funcionario);
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
                if (FuncionarioRepository.BuscarPorId(id) == null)
                    return NotFound();
                FuncionarioRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}