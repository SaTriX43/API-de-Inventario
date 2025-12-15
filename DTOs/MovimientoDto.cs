using API_de_Inventario.Models;
using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.DTOs
{
    public class MovimientoDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
