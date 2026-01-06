using API_de_Inventario.Models;

namespace API_de_Inventario.DALs.ProductoRepositoryCarpeta
{
    public interface IProductoRepository
    {
        public Task<Producto?> ObtenerProductoPorNombreAsync(string nombre);
        public Task<Producto?> ObtenerProductoPorIdAsync(int productoId);
        public Task<List<Producto>> ObtenerProductosAsync();
        public Producto CrearProducto(Producto producto);
    }
}
