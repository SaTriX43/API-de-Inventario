using API_de_Inventario.DTOs.AutenticacionDtoCarpeta;
using API_de_Inventario.Services.AutenticacionServiceCarpeta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Inventario.Controllers
{
    [ApiController]
    [Route("api/autenticacion")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionService _service;

        public AutenticacionController(IAutenticacionService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);
            if (result.IsFailure)
                return Unauthorized(result.Error);

            return Ok(result.Value);
        }
    }

}
