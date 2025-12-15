using API_de_Inventario.Models;
using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.DTOs
{
    public class MovimientoCrearDto
    {
        [Required(ErrorMessage = "El productoId es obligatorio")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "El Tipo de movimiento es obligatorio")]
        public TipoMovimiento Tipo { get; set; }
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }
    }
}
