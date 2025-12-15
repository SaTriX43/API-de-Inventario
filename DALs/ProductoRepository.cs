using API_de_Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.DALs
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context) { _context = context; }

        public async Task<Producto?> ObtenerProductoPorNombre(string nombre)
        {
            var productoEncontrado = await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);
            return productoEncontrado;
        }

        public async Task<Producto?> ObtenerProductoPorId(int productoId)
        {
            var productoEncontrado = await _context.Productos.FirstOrDefaultAsync(p => p.Id == productoId);
            return productoEncontrado;
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }
    }
}
