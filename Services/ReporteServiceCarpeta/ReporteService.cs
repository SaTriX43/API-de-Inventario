using API_de_Inventario.DALs;
using API_de_Inventario.DALs.MovimientoRepositoryCarpeta;
using API_de_Inventario.DALs.ProductoRepositoryCarpeta;
using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using API_de_Inventario.DTOs.ReporteDtoCarpeta;
using API_de_Inventario.Models.Enums;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.ReporteServiceCarpeta
{
    public class ReporteService : IReporteService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IProductoRepository _productoRepository;

        public ReporteService(IMovimientoRepository movimientoRepository, IProductoRepository productoRepository, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _movimientoRepository = movimientoRepository;
            _productoRepository = productoRepository;
        }
        public async Task<Result<int>> ObtenerStockActualAsync(int productoId)
        {

            if (productoId <= 0)
            {
                return Result<int>.Failure("El producto id no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorIdAsync(productoId);

            if (productoExiste == null)
            {
                return Result<int>.Failure($"El producto con id = {productoId} no existe");
            }

            var movimientos = await _movimientoRepository.ObtenerMovimientosPorProductoAsync(productoId);


            int stockActual = 0;

            foreach (var movimientoActual in movimientos)
            {
                if (movimientoActual.Tipo == TipoMovimiento.Entrada)
                {
                    stockActual += movimientoActual.Cantidad;
                }
                else
                {
                    stockActual -= movimientoActual.Cantidad;
                }
            }

            return Result<int>.Success(stockActual);
        }

        public async Task<Result<List<MovimientoDto>>> ObtenerHistorialAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada, int page, int pageSize)
        {
            if (productoId <= 0)
            {
                return Result<List<MovimientoDto>>.Failure("El producto Id no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorIdAsync(productoId);

            if (productoExiste == null)
            {
                return Result<List<MovimientoDto>>.Failure($"El producto con id = {productoId} no existe");
            }

            var movimientosModel = await _movimientoRepository.ObtenerMovimientosPorProductoConFiltrosAsync(productoId, fechaInicio, fechaFinal, tipoEntrada, page, pageSize);

            var movimientosDtos = movimientosModel.Select(m => new MovimientoDto
            {
                Id = m.Id,
                Cantidad = m.Cantidad,
                FechaMovimiento = m.FechaMovimiento,
                ProductoId = m.ProductoId,
                Tipo = m.Tipo
            }).ToList();

            return Result<List<MovimientoDto>>.Success(movimientosDtos);
        }

        public async Task<Result<List<StockActualRespuestaDto>>> ObtenerProductosStockActualAsync()
        {
            var productos = await _productoRepository.ObtenerProductosAsync();

            var stockActualRespuestaDto = new List<StockActualRespuestaDto>();

            foreach(var producto in productos)
            {
                var productoId = producto.Id;
                var productoName = producto.Nombre;

                var stockActualResult = await ObtenerStockActualAsync(productoId);
                var stockActual = stockActualResult.Value;
                var ultimoMovimiento = await _movimientoRepository.ObtenerUltimoMovimientoPorProductoId(productoId);
                var fechaUltimoMovimiento = ultimoMovimiento.FechaMovimiento;

                

                stockActualRespuestaDto.Add(new StockActualRespuestaDto
                {
                    ProductoId = productoId,
                    ProductoNombre = producto.Nombre,
                    StockActual = stockActual,
                    FechaUltimoMovimiento = fechaUltimoMovimiento,
                });
            }

            return Result<List<StockActualRespuestaDto>>.Success(stockActualRespuestaDto);
        }
    }
}
