using API_de_Inventario.Models;

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
    }
}
