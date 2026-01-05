using API_de_Inventario.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.DTOs.MovimientoDtoCarpeta
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
