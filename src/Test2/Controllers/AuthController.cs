using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Test2.Models;

namespace Test2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("qwertyuioplkjhgfdsazxcvbnm");
        }

        [HttpPost]
        public IActionResult Post(Auth payload)
        {
            return Ok(payload.Username);
        }
    }
}
