using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Optus.WebApi.Repositories;

namespace Senai.Optus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {

        EstiloRepository EstiloRepository = new EstiloRepository();
        ArtistaRepository ArtistaRepository = new ArtistaRepository();

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return Ok(new { qtdEstilos = EstiloRepository.QuantidadeEstilos(), qtdArtistas = ArtistaRepository.QuantidadeArtistas() });
        }
    }
}