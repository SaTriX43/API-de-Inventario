using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using API_de_Inventario.Services.MovimientoServiceCarpeta;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

            var movimientoEntradaCreado = await _movimientoService.CrearMovimientoAsync(movimientoCrearEntrada);

            if(movimientoEntradaCreado.IsFailure)
            {
                if(movimientoEntradaCreado.Error.Contains("no existe"))
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
        [Authorize]
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

            var movimientoSalidaCreado = await _movimientoService.CrearMovimientoAsync(movimientoCrearSalida);

            if (movimientoSalidaCreado.IsFailure)
            {
                if (movimientoSalidaCreado.Error.Contains("no existe"))
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
        [Authorize]
        [HttpGet("obtener-stock-actual/{productoId}")]
        public async Task<IActionResult> ObtenerStockActual(int productoId)
        {
            var stockActual = await _movimientoService.ObtenerStockActualAsync(productoId);

            if(stockActual.IsFailure)
            {
                if (stockActual.Error.Contains("no existe"))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = stockActual.Error
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    error = stockActual.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = stockActual.Value
            });
        }
        [Authorize]
        [HttpGet("obtener-historial/{productoId}")]
        public async Task<IActionResult> ObtenerHistorial(
            int productoId,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFinal,
            [FromQuery] bool? tipoEntrada,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var historial = await _movimientoService.ObtenerHistorialAsync(productoId, fechaInicio, fechaFinal,tipoEntrada ,page, pageSize);

            if (historial.IsFailure)
            {
                if (historial.Error.Contains("no existe"))
                {
                    return NotFound(new
                    {
                        success = false,
                        error = historial.Error
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    error = historial.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = historial.Value
            });
        }
    }
}
