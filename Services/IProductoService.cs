using API_de_Inventario.DTOs;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services
{
    public interface IProductoService
    {
        public Task<Result<ProductoDto>> CrearProducto(ProductoCrearDto productoCrear);
    }
}
