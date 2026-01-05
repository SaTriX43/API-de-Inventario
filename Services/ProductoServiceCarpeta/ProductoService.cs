using API_de_Inventario.DALs;
using API_de_Inventario.DALs.ProductoRepositoryCarpeta;
using API_de_Inventario.DTOs.ProductoDtoCarpeta;
using API_de_Inventario.Models;
using InventarioAPI.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_de_Inventario.Services.ProductoServiceCarpeta
{
    public class ProductoService : IProductoService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _productoRepository = productoRepository;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<Result<ProductoDto>> CrearProductoAsync(ProductoCrearDto productoCrearDto)
        {
            var productoNombreNormalizado = productoCrearDto.Nombre.Trim().ToLower();
            var productoExistente = await _productoRepository.ObtenerProductoPorNombreAsync(productoNombreNormalizado);

            if (productoExistente != null) {
                return Result<ProductoDto>.Failure($"El producto con nombre = {productoNombreNormalizado} ya existe");
            }

            var productoCrearModel = new Producto
            {
                Nombre = productoNombreNormalizado,
                Descripcion = productoCrearDto.Descripcion,
                Precio = productoCrearDto.Precio,
            };

            var productoCreado = _productoRepository.CrearProducto(productoCrearModel);

            await _unidadDeTrabajo.GuardarCambiosAsync();

            var productoCreadoDto = new ProductoDto
            {
                Id = productoCrearModel.Id,
                Nombre = productoCreado.Nombre,
                Descripcion = productoCreado.Descripcion,
                FechaCreacion = productoCreado.FechaCreacion,
                Precio = productoCreado.Precio
            };

            return Result<ProductoDto>.Success(productoCreadoDto);
        }
    }
}
