using API_de_Inventario.Models;
using System.Net;

namespace API_de_Inventario.DALs.MovimientoRepositoryCarpeta
{
    public interface IMovimientoRepository
    {
        public Movimiento CrearMovimiento(Movimiento movimiento);

        public Task<List<Movimiento>> ObtenerMovimientosPorProductoAsync(int productoId);
        public Task<List<Movimiento>> ObtenerMovimientosPorProductoConFiltrosAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal,bool? tipoEntrada ,int page, int pageSize);
        public Task<Movimiento?> ObtenerUltimoMovimientoPorProductoId(int productoId);
    }
}
