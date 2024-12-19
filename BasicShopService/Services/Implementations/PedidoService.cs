using BasicShopService.DTOs.Pedidos;
using BasicShopService.Models;
using BasicShopService.Repositories.Implementations;
using BasicShopService.Repositories.Interfaces;
using BasicShopService.Services.Interfaces;
using Proyecto.Models;

namespace BasicShopService.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoProductoRepository _pedidoProductoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IPedidoProductoRepository pedidoProductoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoProductoRepository = pedidoProductoRepository;

        }

        public async Task<Pedido> CrearPedido(Pedido pedido, List<PedidoProducto> productos)
        {
            var nuevoPedido = await _pedidoRepository.Add(pedido);

            foreach (var producto in productos)
            {
                producto.PedidoId = nuevoPedido.Id;
                await _pedidoProductoRepository.Add(producto);
            }

            return nuevoPedido;
        }

        public async Task<Pedido> ObtenerPedidoPorId(int id)
        {
            return await _pedidoRepository.GetById(id);
        }

        public async Task<IEnumerable<Pedido>> ObtenerPedidos()
        {
            return await _pedidoRepository.GetAll();
        }

        public async Task<IEnumerable<PedidoProducto>> ObtenerProductosPorPedidoId(int pedidoId)
        {
            return await _pedidoProductoRepository.GetByPedidoId(pedidoId);
        }

        public async Task<Pedido> ActualizarEstadoPedido(int id, EstadoPedido nuevoEstado)
        {
            return await _pedidoRepository.UpdateEstado(id, nuevoEstado);
        }

        public async Task<bool> EliminarPedido(int id)
        {
            var productos = await _pedidoProductoRepository.GetByPedidoId(id);
            foreach (var producto in productos)
            {
                await _pedidoProductoRepository.Delete(producto.Id);
            }

            return await _pedidoRepository.Delete(id);
        }

        public async Task<PedidoConDetalleDTO> ObtenerPedidoConDetalle(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetById(pedidoId);
            if (pedido == null) return null;

            var productos = await _pedidoProductoRepository.GetByPedidoId(pedidoId);

            var pedidoConDetalle = new PedidoConDetalleDTO
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                FechaPedido = pedido.FechaPedido,
                FechaDespacho = pedido.FechaDespacho,
                FechaEntrega = pedido.FechaEntrega,
                VendedorId = pedido.VendedorId,
                DeliveryId = pedido.DeliveryId,
                Estado = pedido.Estado,
                Productos = productos.Select(p => new PedidoProductoDetalleDTO
                {
                    ProductoId = p.ProductoId,
                    Cantidad = p.Cantidad,
                    NombreProducto = p.Producto.Nombre,
                    PrecioProducto = p.Producto.Precio
                }).ToList()
            };

            return pedidoConDetalle;
        }

        public async Task<IEnumerable<Pedido>> ListarTodosLosPedidos(string? numeroPedido = null)
        {
            // Obtener todos los pedidos
            var pedidos = await _pedidoRepository.GetAll();

            // Aplicar el filtro por NumeroPedido si se proporciona
            if (!string.IsNullOrEmpty(numeroPedido))
            {
                pedidos = pedidos.Where(p => p.NumeroPedido == numeroPedido).ToList();
            }

            return pedidos;
        }

    }
}
