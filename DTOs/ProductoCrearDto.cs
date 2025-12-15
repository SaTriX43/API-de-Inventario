using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.DTOs
{
    public class ProductoCrearDto
    {
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public decimal Precio {  get; set; }

    }
}
