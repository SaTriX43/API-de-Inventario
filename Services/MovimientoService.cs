using API_de_Inventario.DALs;
using API_de_Inventario.DTOs;
using API_de_Inventario.Models;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IProductoRepository _productoRepository;

        public MovimientoService(IMovimientoRepository movimientoRepository, IProductoRepository productoRepository)
        {
            _movimientoRepository = movimientoRepository;
            _productoRepository = productoRepository;
        }

        public async Task<Result<MovimientoDto>> CrearMovimiento(MovimientoCrearDto movimientoCrearDto)
        {

            if(movimientoCrearDto.ProductoId <= 0)
            {
                return Result<MovimientoDto>.Failure("El productId no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorId(movimientoCrearDto.ProductoId);

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

                var stockActual = await ObtenerStockActual(movimientoCrearDto.ProductoId);

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

            var crearMovimientoEntrada = await _movimientoRepository.CrearMovimiento(movimientoEntradaModel);

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

        public async Task<Result<int>> ObtenerStockActual(int productoId)
        {

            if(productoId <= 0)
            {
                return Result<int>.Failure("El producto id no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorId(productoId);

            if (productoExiste == null)
            {
                return Result<int>.Failure($"El producto con id = {productoId} no existe");
            }

            var movimientos = await _movimientoRepository.ObtenerMovimientosPorProducto(productoId);


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
    }
}
