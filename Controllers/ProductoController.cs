using API_de_Inventario.DALs;
using API_de_Inventario.DTOs;
using API_de_Inventario.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService) { 
            _productoService = productoService;
        }

        [HttpPost("crear-producto")]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoCrearDto productoCrearDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var productoCreado = await _productoService.CrearProducto(productoCrearDto);

            if(productoCreado.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = productoCreado.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = productoCreado.Value
            });
         }

    }
}
