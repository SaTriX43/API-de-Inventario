using API_de_Inventario.DTOs.MovimientoDtoCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.MovimientoServiceCarpeta
{
    public interface IMovimientoService
    {
        public Task<Result<MovimientoDto>> CrearMovimientoAsync(MovimientoCrearDto movimientoCrearDto);
        
    }
}
