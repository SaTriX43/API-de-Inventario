using API_de_Inventario.Models;

namespace API_de_Inventario.DALs
{
    public interface IProductoRepository
    {
        public Task<Producto?> ObtenerProductoPorNombre(string nombre);
        public Task<Producto?> ObtenerProductoPorId(int productoId);
        public Task<Producto> CrearProducto(Producto producto);
    }
}
