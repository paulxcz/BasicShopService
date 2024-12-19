using BasicShopService.Models;

namespace BasicShopService.Repositories.Interfaces
{
    public interface IPedidoProductoRepository
    {
        Task<PedidoProducto> Add(PedidoProducto pedidoProducto);
        Task<IEnumerable<PedidoProducto>> GetByPedidoId(int pedidoId);
        Task<IEnumerable<PedidoProducto>> GetByProductoId(int productoId);
        Task<bool> Delete(int pedidoProductoId);
    }
}
