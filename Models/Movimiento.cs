using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.Models
{
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
