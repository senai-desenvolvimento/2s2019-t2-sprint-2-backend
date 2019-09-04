using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        EstudioRepository EstudioRepository = new EstudioRepository();
        
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(EstudioRepository.Listar());
        }

        [HttpGet("jogos")]
        public IActionResult ListarEstudiosComJogos()
        {
            return Ok(EstudioRepository.ListarEstudiosComJogos());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Estudios estudio = EstudioRepository.BuscarPorId(id);
                if (estudio == null)
                    return NotFound();
                return Ok(estudio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("meusestudios")]
        public IActionResult BuscarMeusEstudios()
        {
            try
            {
                int UsuarioIdDoToken = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                List<Estudios> estudios = EstudioRepository.BuscarMeusEstudios(UsuarioIdDoToken);
                if (estudios.Count == 0)
                    return NotFound();
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("datacriacao/antigos")]
        public IActionResult BuscarAntigos()
        {
            try
            {
                List<Estudios> estudios = EstudioRepository.Listar().Where(x => x.DataCriacao < DateTime.Now.AddDays(-10)).ToList();
                if (estudios.Count == 0)
                    return NotFound();
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("usuarios")]
        public IActionResult ListarEstudiosComUsuarios()
        {
            try
            {
                List<Estudios> estudios = EstudioRepository.ListarEstudiosComUsuarios();
                if (estudios.Count == 0)
                    return NotFound();
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            try
            {
                List<Estudios> estudio = EstudioRepository.BuscarPorNome(nome);
                if (estudio.Count == 0)
                    return NotFound();
                return Ok(estudio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("buscar/{nome}/pais")]
        public IActionResult BuscarPorPais(string nome)
        {
            try
            {
                List<Estudios> estudio = EstudioRepository.BuscarPorPais(nome);
                if (estudio.Count == 0)
                    return NotFound();
                return Ok(estudio);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Estudios estudio)
        {
            try
            {
                // para cadastrar um novo estudio, pego o id do token e a data atual, nao sendo necessario o usuario informar
                int UsuarioIdDoToken = Convert.ToInt32(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                estudio.UsuarioId = UsuarioIdDoToken;
                estudio.DataCriacao = DateTime.Now;
                EstudioRepository.Cadastrar(estudio);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                EstudioRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Estudios estudio)
        {
            try
            {
                Estudios EstudioBuscado = EstudioRepository.BuscarPorId(id);

                if (EstudioBuscado == null)
                    return NotFound();

                EstudioRepository.Atualizar(id, estudio);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }
    }
}