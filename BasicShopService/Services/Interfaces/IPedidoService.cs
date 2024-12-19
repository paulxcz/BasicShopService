using BasicShopService.DTOs.Pedidos;
using BasicShopService.Models;
using Proyecto.Models;

namespace BasicShopService.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> CrearPedido(Pedido pedido, List<PedidoProducto> productos);
        Task<Pedido> ObtenerPedidoPorId(int id);
        Task<IEnumerable<Pedido>> ObtenerPedidos();
        Task<IEnumerable<PedidoProducto>> ObtenerProductosPorPedidoId(int pedidoId);
        Task<Pedido> ActualizarEstadoPedido(int id, EstadoPedido nuevoEstado);
        Task<PedidoConDetalleDTO> ObtenerPedidoConDetalle(int pedidoId);
        Task<IEnumerable<Pedido>> ListarTodosLosPedidos(string? numeroPedido = null);
        Task<bool> EliminarPedido(int id);
    }
}
