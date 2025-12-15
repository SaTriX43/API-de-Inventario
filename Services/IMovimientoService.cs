using API_de_Inventario.DTOs;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services
{
    public interface IMovimientoService
    {
        public Task<Result<MovimientoDto>> CrearMovimiento(MovimientoCrearDto movimientoCrearDto);
        public Task<Result<int>> ObtenerStockActual(int productoId);
        public Task<Result<List<MovimientoDto>>> ObtenerHistorial(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada ,int page, int pageSize);
    }
}
