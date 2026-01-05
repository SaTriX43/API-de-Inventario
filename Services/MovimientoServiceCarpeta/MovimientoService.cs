using API_de_Inventario.DALs;
using API_de_Inventario.DALs.MovimientoRepositoryCarpeta;
using API_de_Inventario.DALs.ProductoRepositoryCarpeta;
using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using API_de_Inventario.Models;
using API_de_Inventario.Models.Enums;
using API_de_Inventario.Services.ReporteServiceCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.MovimientoServiceCarpeta
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IReporteService _reporteService;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IProductoRepository _productoRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository, IProductoRepository productoRepository, IUnidadDeTrabajo unidadDeTrabajo,IReporteService reporteService)
        {
            _movimientoRepository = movimientoRepository;
            _productoRepository = productoRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
            _reporteService = reporteService;
        }

        public async Task<Result<MovimientoDto>> CrearMovimientoAsync(MovimientoCrearDto movimientoCrearDto)
        {

            if(movimientoCrearDto.ProductoId <= 0)
            {
                return Result<MovimientoDto>.Failure("El productId no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorIdAsync(movimientoCrearDto.ProductoId);

            if (productoExiste == null)
            {
                return Result<MovimientoDto>.Failure($"El producto con id = {movimientoCrearDto.ProductoId} no existe");
            }

            if(movimientoCrearDto.Cantidad <= 0)
            {
                return Result<MovimientoDto>.Failure("La cantidad no puede ser menor o igual a 0");
            }

            if(movimientoCrearDto.Tipo == TipoMovimiento.Salida)
            {

                var stockActual = await _reporteService.ObtenerStockActualAsync(movimientoCrearDto.ProductoId);

                if(movimientoCrearDto.Cantidad > stockActual.Value)
                {
                    return Result<MovimientoDto>.Failure("El stock actual es menor a la cantidad que quieres quitar");
                }
            }

            var movimientoEntradaModel = new Movimiento
            {
                ProductoId = movimientoCrearDto.ProductoId,
                Cantidad = movimientoCrearDto.Cantidad,
                Tipo = movimientoCrearDto.Tipo,
                FechaMovimiento = DateTime.UtcNow,
            };

            var crearMovimientoEntrada = _movimientoRepository.CrearMovimiento(movimientoEntradaModel);

            await _unidadDeTrabajo.GuardarCambiosAsync();

            var movimientoCreadoDto = new MovimientoDto
            {
                Id = crearMovimientoEntrada.Id,
                Cantidad = crearMovimientoEntrada.Cantidad,
                FechaMovimiento = crearMovimientoEntrada.FechaMovimiento,
                ProductoId = crearMovimientoEntrada.ProductoId,
                Tipo = crearMovimientoEntrada.Tipo
            };

            return Result<MovimientoDto>.Success(movimientoCreadoDto);
        }
    }
}
