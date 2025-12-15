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



        public async Task<List<Movimiento>> ObtenerMovimientosPorProductoConFiltros(int productoId, DateTime? fechaInicio, DateTime? fechaFinal, bool? tipoEntrada, int page, int pageSize)
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

        public async Task<List<Movimiento>> ObtenerMovimientosPorProducto(int productoId)
        {
            var movimientos = await _context.Movimientos.Where(m => m.ProductoId == productoId).ToListAsync();
            return movimientos;
        }
    }
}
