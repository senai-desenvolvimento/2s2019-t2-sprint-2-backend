using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.ManualPecas.WebApi.Domains;
using Senai.ManualPecas.WebApi.Repositories;
using Senai.ManualPecas.WebApi.ViewModels;

namespace Senai.ManualPecas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private FornecedorRepository FornecedorRepository = new FornecedorRepository();

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Fornecedores fornecedor = FornecedorRepository.BuscarPorCNPJeSenha(login);

                if (fornecedor == null)
                    return NoContent();

                var claims = new[]
                {
                    // id
                    new Claim(JwtRegisteredClaimNames.Jti, fornecedor.FornecedorId.ToString()),
                    new Claim("CNPJ", fornecedor.Cnpj)
                };
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ManualPecas-chave-autenticacao"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "ManualPecas.WebApi",
                    audience: "ManualPecas.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception e)
            {
                return BadRequest(new { mensagem = "Erro." + e.Message });
            }

        }
    }
}
