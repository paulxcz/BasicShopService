using BasicShopService.Models;
using BasicShopService.Repositories.Interfaces;
using BasicShopService.Services.Interfaces;

namespace BasicShopService.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            return await _productoRepository.Add(producto);
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await _productoRepository.GetById(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerProductos()
        {
            return await _productoRepository.GetAll();
        }

        public async Task<IEnumerable<Producto>> FiltrarProductos(string criterio)
        {
            return await _productoRepository.FindByCriteria(criterio);
        }

        public async Task<Producto> ActualizarProducto(Producto producto)
        {
            return await _productoRepository.Update(producto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            return await _productoRepository.Delete(id);
        }
    }
}
