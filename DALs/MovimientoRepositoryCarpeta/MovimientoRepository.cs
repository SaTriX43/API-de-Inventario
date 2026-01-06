using API_de_Inventario.Models;
using API_de_Inventario.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.DALs.MovimientoRepositoryCarpeta
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDbContext _context;

        public MovimientoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movimiento CrearMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);   
            return movimiento;
        }
        public async Task<List<Movimiento>> ObtenerMovimientosPorProductoConFiltrosAsync(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada, int page, int pageSize)
        {
            var query = _context.Movimientos.AsQueryable();

            if(fechaInicio.HasValue)
            {
                query = query.Where(m => m.FechaMovimiento >= fechaInicio.Value);
            }

            if (fechaFinal.HasValue)
            {
                query = query.Where(m => m.FechaMovimiento <= fechaFinal.Value);
            }

            if (tipoEntrada != null) {
                if(tipoEntrada.Value)
                {
                    query = query.Where(m => m.Tipo == TipoMovimiento.Entrada);
                }else
                {
                    query = query.Where(m => m.Tipo == TipoMovimiento.Salida);
                }
            }


            query = query.OrderByDescending(m => m.FechaMovimiento).Skip((page - 1) * pageSize).Take(pageSize) ;

            

            return await query.ToListAsync();
        }
        public async Task<List<Movimiento>> ObtenerMovimientosPorProductoAsync(int productoId)
        {
            var movimientos = await _context.Movimientos.Where(m => m.ProductoId == productoId).ToListAsync();
            return movimientos;
        }

        public async Task<Movimiento?> ObtenerUltimoMovimientoPorProductoId(int productoId)
        {
            var movimiento = await _context.Movimientos.Where(m => m.ProductoId == productoId).OrderByDescending(m => m.FechaMovimiento).FirstOrDefaultAsync();
            return movimiento;
        }
    }
}
