using BasicShopService.DTOs.Productos;
using BasicShopService.Models;
using BasicShopService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using System.Data;

namespace BasicShopService.Controllers
{
    [Authorize(Roles = "Encargado")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _productoService.ObtenerProductos();

            var productosDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                SKU = p.SKU,
                Nombre = p.Nombre,
                Tipo = p.Tipo,
                Etiqueta = p.Etiqueta,
                Precio = p.Precio,
                UnidadStock = p.UnidadStock
            });

            return Ok(productosDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _productoService.ObtenerProductoPorId(id);
            if (producto == null)
                return NotFound();

            var productoDTO = new ProductoDTO
            {
                Id = producto.Id,
                SKU = producto.SKU,
                Nombre = producto.Nombre,
                Tipo = producto.Tipo,
                Etiqueta = producto.Etiqueta,
                Precio = producto.Precio,
                UnidadStock = producto.UnidadStock
            };

            return Ok(productoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> CrearProducto([FromBody] CrearProductoDTO crearProductoDTO)
        {
            var producto = new Producto
            {
                SKU = crearProductoDTO.SKU,
                Nombre = crearProductoDTO.Nombre,
                Tipo = crearProductoDTO.Tipo,
                Etiqueta = crearProductoDTO.Etiqueta,
                Precio = crearProductoDTO.Precio,
                UnidadStock = crearProductoDTO.UnidadStock
            };

            var nuevoProducto = await _productoService.CrearProducto(producto);

            var productoDTO = new ProductoDTO
            {
                Id = nuevoProducto.Id,
                SKU = nuevoProducto.SKU,
                Nombre = nuevoProducto.Nombre,
                Tipo = nuevoProducto.Tipo,
                Etiqueta = nuevoProducto.Etiqueta,
                Precio = nuevoProducto.Precio,
                UnidadStock = nuevoProducto.UnidadStock
            };

            return CreatedAtAction(nameof(GetProducto), new { id = productoDTO.Id }, productoDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ActualizarProductoDTO actualizarProductoDTO)
        {
            var productoExistente = await _productoService.ObtenerProductoPorId(id);
            if (productoExistente == null)
                return NotFound();

            productoExistente.SKU = actualizarProductoDTO.SKU;
            productoExistente.Nombre = actualizarProductoDTO.Nombre;
            productoExistente.Tipo = actualizarProductoDTO.Tipo;
            productoExistente.Etiqueta = actualizarProductoDTO.Etiqueta;
            productoExistente.Precio = actualizarProductoDTO.Precio;
            productoExistente.UnidadStock = actualizarProductoDTO.UnidadStock;

            await _productoService.ActualizarProducto(productoExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var resultado = await _productoService.EliminarProducto(id);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> FiltrarProductos([FromQuery] string criterio)
        {
            var productos = await _productoService.FiltrarProductos(criterio);

            var productosDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                SKU = p.SKU,
                Nombre = p.Nombre,
                Tipo = p.Tipo,
                Etiqueta = p.Etiqueta,
                Precio = p.Precio,
                UnidadStock = p.UnidadStock
            });

            return Ok(productosDTO);
        }
    }
}
