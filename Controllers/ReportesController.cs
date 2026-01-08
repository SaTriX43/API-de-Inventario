using API_de_Inventario.Services.ReporteServiceCarpeta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [Authorize]
        [HttpGet("obtener-stock-actual/{productoId}")]
        public async Task<IActionResult> ObtenerStockActual(int productoId)
        {
            var stockActual = await _reporteService.ObtenerStockActualAsync(productoId);

            if (stockActual.IsFailure)
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
        [HttpGet("obtener-productos-stock-actual")]
        public async Task<IActionResult> ObtenerProductosStockActual()
        {
            var stockActual = await _reporteService.ObtenerProductosStockActualAsync();

            if (stockActual.IsFailure)
            {
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

        [Authorize(Roles = "Admin")]
        [HttpGet("obtener-productos-stock-bajo")]
        public async Task<IActionResult> ObtenerProductosStockBajo()
        {
            var stockActual = await _reporteService.ObtenerProductosConStockBajo();

            if (stockActual.IsFailure)
            {
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
            var historial = await _reporteService.ObtenerHistorialAsync(productoId, fechaInicio, fechaFinal, tipoEntrada, page, pageSize);

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
