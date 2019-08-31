using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Senai.Auth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MensagensController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Mensagem()
        {
            return Ok(new { mensagem = "Sucesso." });
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("administrador")]
        public IActionResult Administrador()
        {
            return Ok(new { mensagem = "Sucesso." });
        }

        [Authorize]
        [HttpGet("dados")]
        public IActionResult DadosDoToken()
        {
            int UsuarioId = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            string UsuarioPermissao = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            return Ok(new { UsuarioId = UsuarioId, Permissao = UsuarioPermissao });
        }
    }
}