using API_de_Inventario.DTOs.AutenticacionDtoCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.AutenticacionServiceCarpeta
{
    public interface IAutenticacionService
    {
        Task<Result<AutenticacionRespuestaDto>> RegisterAsync(RegisterDto dto);
        Task<Result<AutenticacionRespuestaDto>> LoginAsync(LoginDto dto);
    }

}
