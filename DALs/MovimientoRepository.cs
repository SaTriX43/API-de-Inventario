using API_de_Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.DALs
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDbContext _context;

        public MovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movimiento> CrearMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);   
            await _context.SaveChangesAsync();
            return movimiento;
        }

        public async Task<List<Movimiento>> ObtenerMovimientosPorProducto(int productoId)
        {
            var movimientosDelProducto = await _context.Movimientos.Where(m => m.ProductoId == productoId).ToListAsync();
            return movimientosDelProducto;
        }
    }
}
