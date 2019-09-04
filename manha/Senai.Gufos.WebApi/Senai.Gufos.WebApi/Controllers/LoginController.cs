using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;
using Senai.Gufos.WebApi.ViewModels;

namespace Senai.Gufos.WebApi.Controllers
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
                Usuarios usuarioBuscado = UsuarioRepository.BuscarPorEmailESenha(login);
                if (usuarioBuscado == null)
                    return NotFound(new { mensagem = "Email ou Senha Inválidos." });

                // informacoes referentes ao usuario
                var claims = new[]
               {
                    // chave customizada
                    new Claim("chave", "0123456789"),
                    new Claim("mari", "AgoraFoi"),
                    // email
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    // id
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    // permissao
                    new Claim(ClaimTypes.Role, usuarioBuscado.Permissao),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("gufos-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Gufos.WebApi",
                    audience: "Gufos.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao cadastrar." + ex.Message });
            }
        }
    }
}