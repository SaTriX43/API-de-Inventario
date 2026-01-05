using API_de_Inventario.DTOs;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.MovimientoServiceCarpeta
{
    public interface IMovimientoService
    {
        public Task<Result<MovimientoDto>> CrearMovimientoAsync(MovimientoCrearDto movimientoCrearDto);
        public Task<Result<int>> ObtenerStockActualAsync(int productoId);
        public Task<Result<List<MovimientoDto>>> ObtenerHistorialAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada ,int page, int pageSize);
    }
}
