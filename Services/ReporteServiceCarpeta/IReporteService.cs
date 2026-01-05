using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.ReporteServiceCarpeta
{
    public interface IReporteService
    {
        public Task<Result<int>> ObtenerStockActualAsync(int productoId);
        public Task<Result<List<MovimientoDto>>> ObtenerHistorialAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada, int page, int pageSize);
    }
}
