using API_de_Inventario.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API_de_Inventario.DTOs.AutenticacionDtoCarpeta
{
    public class RegisterDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;

    }
}
