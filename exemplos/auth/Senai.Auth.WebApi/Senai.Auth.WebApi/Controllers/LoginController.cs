using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Auth.WebApi.Domains;
using Senai.Auth.WebApi.Repositories;
using Senai.Auth.WebApi.ViewModels;

namespace Senai.Auth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsuarioRepository UsuarioRepository = new UsuarioRepository();

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                // Usuarios Usuario = UsuarioRepository.BuscarPorEmailESenhaSqlClient(login);
                Usuarios Usuario = UsuarioRepository.BuscarPorEmailESenha(login);
                if (Usuario == null)
                    return NotFound(new { mensagem = "Email ou senha inválidos." });

                // informacoes do usuario
                var claims = new[]
                {
                    // email
                    new Claim(JwtRegisteredClaimNames.Email, Usuario.Email),
                    // id
                    new Claim(JwtRegisteredClaimNames.Jti, Usuario.UsuarioId.ToString()),
                    // é a permissão do usuário
                    new Claim(ClaimTypes.Role, Usuario.Permissao.Nome),
                };

                // chave que tambem esta configurada no startup
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("auth-chave-autenticacao"));

                // criptografia
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // eh o proprio token 
                var token = new JwtSecurityToken(
                    // quem esta mandando e quem esta validando
                    issuer: "Auth.WebApi",
                    audience: "Auth.WebApi",
                    // sao as informacoes do usuario
                    claims: claims,
                    // data de expiracao
                    expires: DateTime.Now.AddDays(30),
                    // eh a chave
                    signingCredentials: creds);

                // gerar a chave pra vocês
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro." + ex.Message });
            }
        }
    }
}