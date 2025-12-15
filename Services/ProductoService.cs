using API_de_Inventario.DALs;
using API_de_Inventario.DTOs;
using API_de_Inventario.Models;
using InventarioAPI.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_de_Inventario.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Result<ProductoDto>> CrearProducto(ProductoCrearDto productoCrearDto)
        {
            var productoNombreNormalizado = productoCrearDto.Nombre.Trim().ToLower();
            var productoExistente = await _productoRepository.ObtenerProductoPorNombre(productoNombreNormalizado);

            if (productoExistente != null) {
                return Result<ProductoDto>.Failure($"El producto con nombre = {productoNombreNormalizado} ya existe");
            }

            var productoCrearModel = new Producto
            {
                Nombre = productoNombreNormalizado,
                Descripcion = productoCrearDto.Descripcion,
                Precio = productoCrearDto.Precio,
            };

            var productoCreado = await _productoRepository.CrearProducto(productoCrearModel);

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
