using BasicShopService.Models;

namespace BasicShopService.Services.Interfaces
{
    public interface IProductoService
    {
        Task<Producto> CrearProducto(Producto producto);
        Task<Producto> ObtenerProductoPorId(int id);
        Task<IEnumerable<Producto>> ObtenerProductos();
        Task<IEnumerable<Producto>> FiltrarProductos(string criterio);
        Task<Producto> ActualizarProducto(Producto producto);
        Task<bool> EliminarProducto(int id);
    }
}
