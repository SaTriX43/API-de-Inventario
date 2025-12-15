using API_de_Inventario.DTOs;
using API_de_Inventario.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;

        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpPost("crear-movimiento-entrada")]
        public async Task<IActionResult> CrearMovimientoEntrada([FromBody] MovimientoCrearDto movimientoCrearEntrada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var movimientoEntradaCreado = await _movimientoService.CrearMovimiento(movimientoCrearEntrada);

            if(movimientoEntradaCreado.IsFailure)
            {
                if(movimientoEntradaCreado.Error.Contains("no exite"))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = movimientoEntradaCreado.Error
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    error = movimientoEntradaCreado.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = movimientoEntradaCreado.Value
            });
        }

        [HttpPost("crear-movimiento-salida")]
        public async Task<IActionResult> CrearMovimientoSalida([FromBody] MovimientoCrearDto movimientoCrearSalida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var movimientoSalidaCreado = await _movimientoService.CrearMovimiento(movimientoCrearSalida);

            if (movimientoSalidaCreado.IsFailure)
            {
                if (movimientoSalidaCreado.Error.Contains("no exite"))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = movimientoSalidaCreado.Error
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    error = movimientoSalidaCreado.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = movimientoSalidaCreado.Value
            });
        }
    }
}
