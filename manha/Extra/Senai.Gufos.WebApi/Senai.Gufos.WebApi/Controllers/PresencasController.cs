using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gufos.WebApi.Repositories;

namespace Senai.Gufos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PresencasController : ControllerBase
    {

        PresencaRepository PresencaRepository = new PresencaRepository();

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(PresencaRepository.Listar());
        }
    }
}