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

        public async Task<Result<MovimientoDto>> CrearMovimientoEntrada(MovimientoCrearDto movimientoEntradaCrearDto)
        {
            if(movimientoEntradaCrearDto.Tipo != TipoMovimiento.Entrada)
            {
                return Result<MovimientoDto>.Failure("El tipo de movimiento debe ser de tipo entrada");
            }

            if(movimientoEntradaCrearDto.ProductoId <= 0)
            {
                return Result<MovimientoDto>.Failure("El productId no puede ser menor o igual a 0");
            }

            var productoExiste = await _productoRepository.ObtenerProductoPorId(movimientoEntradaCrearDto.ProductoId);

            if (productoExiste == null)
            {
                return Result<MovimientoDto>.Failure($"El producto con id = {movimientoEntradaCrearDto.ProductoId} no existe");
            }

            var movimientoEntradaModel = new Movimiento
            {
                ProductoId = movimientoEntradaCrearDto.ProductoId,
                Cantidad = movimientoEntradaCrearDto.Cantidad,
                Tipo = movimientoEntradaCrearDto.Tipo
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
    }
}
