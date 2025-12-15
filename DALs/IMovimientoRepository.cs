using API_de_Inventario.Models;

namespace API_de_Inventario.DALs
{
    public interface IMovimientoRepository
    {
        public Task<Movimiento> CrearMovimiento(Movimiento movimiento);
        public Task<List<Movimiento>> ObtenerMovimientosPorProducto(int productoId);
    }
}
