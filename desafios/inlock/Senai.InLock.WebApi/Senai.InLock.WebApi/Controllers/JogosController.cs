using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Repositories;
using Senai.InLock.WebApi.ViewModels;

namespace Senai.InLock.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        JogoRepository JogoRepository = new JogoRepository();

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(JogoRepository.Listar());
        }

        [HttpGet("ordenar/preco")]
        public IActionResult ListarMaisCaros()
        {
            return Ok(JogoRepository.Listar().OrderByDescending(x => x.Valor).Take(5));
        }

        [HttpGet("ordenar/data")]
        public IActionResult ListarPorLancamento()
        {
            return Ok(JogoRepository.Listar().OrderByDescending(x => x.DataLancamento));
        }

        [HttpGet("lancamentos")]
        public IActionResult ListarLancamentos()
        {
            var resultado = JogoRepository.Listar()
            .Select(x => new LancamentoViewModel
            {
                DataLancamento = x.DataLancamento,
                NomeJogo = x.NomeJogo,
                QtdDias = Convert.ToInt32(Convert.ToDateTime(x.DataLancamento).Subtract(DateTime.Now).TotalDays) < 0 ? 0 : Convert.ToInt32(Convert.ToDateTime(x.DataLancamento).Subtract(DateTime.Now).TotalDays)
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Jogos jogo = JogoRepository.BuscarPorId(id);
                if (jogo == null)
                    return NotFound();
                return Ok(jogo);
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
                List<Jogos> jogo = JogoRepository.BuscarPorNome(nome);
                if (jogo.Count == 0)
                    return NotFound();
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Jogos jogo)
        {
            try
            {
                JogoRepository.Cadastrar(jogo);
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
                JogoRepository.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Jogos jogo)
        {
            try
            {
                Jogos JogoBuscado = JogoRepository.BuscarPorId(id);

                if (JogoBuscado == null)
                    return NotFound();

                JogoRepository.Atualizar(id, jogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Ih, deu erro." + ex.Message });
            }
        }
    }
}