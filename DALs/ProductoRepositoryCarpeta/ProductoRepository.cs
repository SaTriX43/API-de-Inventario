using API_de_Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.DALs.ProductoRepositoryCarpeta
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context) { _context = context; }

        public async Task<Producto?> ObtenerProductoPorNombreAsync(string nombre)
        {
            var productoEncontrado = await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);
            return productoEncontrado;
        }
        public async Task<Producto?> ObtenerProductoPorIdAsync(int productoId)
        {
            var productoEncontrado = await _context.Productos.FirstOrDefaultAsync(p => p.Id == productoId);
            return productoEncontrado;
        }
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            var productos = await _context.Productos
                .Include(p => p.Movimientos)
                .ToListAsync();

            return productos;
        }
        public Producto CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            return producto;
        }
    }
}
