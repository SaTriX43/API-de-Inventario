using API_de_Inventario.DTOs.ProductoDtoCarpeta;
using InventarioAPI.Shared;

namespace API_de_Inventario.Services.ProductoServiceCarpeta
{
    public interface IProductoService
    {
        public Task<Result<ProductoDto>> CrearProductoAsync(ProductoCrearDto productoCrear);
    }
}
