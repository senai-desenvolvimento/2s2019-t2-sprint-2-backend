using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gufos.WebApi.Domains;
using Senai.Gufos.WebApi.Repositories;
using Senai.Gufos.WebApi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                    return NotFound(new { mensagem = "Eita, deu ruim." });

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    // é a permissão do usuário
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
            catch (System.Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}