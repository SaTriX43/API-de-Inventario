using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using API_de_Inventario.DTOs.ReporteDtoCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.ReporteServiceCarpeta
{
    public interface IReporteService
    {
        public Task<Result<int>> ObtenerStockActualAsync(int productoId);
        public Task<Result<List<MovimientoDto>>> ObtenerHistorialAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada, int page, int pageSize);
        public Task<Result<List<StockRespuestaDto>>> ObtenerProductosStockActualAsync();
        public Task<Result<List<StockRespuestaDto>>> ObtenerProductosConStockBajo();
    }
}
