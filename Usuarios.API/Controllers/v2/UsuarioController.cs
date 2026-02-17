using Microsoft.AspNetCore.Mvc;

namespace Usuarios.API.Controllers.v2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Ola Mundo!");
        }
    }
}
