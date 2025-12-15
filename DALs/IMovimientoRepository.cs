using API_de_Inventario.Models;
using System.Net;

namespace API_de_Inventario.DALs
{
    public interface IMovimientoRepository
    {
        public Task<Movimiento> CrearMovimiento(Movimiento movimiento);

        public Task<List<Movimiento>> ObtenerMovimientosPorProducto(int productoId);
        public Task<List<Movimiento>> ObtenerMovimientosPorProductoConFiltros(int productoId, DateTime? fechaInicio, DateTime? fechaFinal,bool? tipoEntrada ,int page, int pageSize);
    }
}
