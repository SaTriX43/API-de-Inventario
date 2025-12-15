using API_de_Inventario.DTOs;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services
{
    public interface IMovimientoService
    {
        public Task<Result<MovimientoDto>> CrearMovimiento(MovimientoCrearDto movimientoCrearDto);
    }
}
