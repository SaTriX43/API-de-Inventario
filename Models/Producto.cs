using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    }

    
}
