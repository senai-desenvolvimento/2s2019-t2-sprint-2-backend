using Microsoft.AspNetCore.Mvc;

namespace Senai.Swagger.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ExemplosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}