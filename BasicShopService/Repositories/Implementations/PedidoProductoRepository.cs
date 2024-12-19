using BasicShopService.Data;
using BasicShopService.Models;
using BasicShopService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasicShopService.Repositories.Implementations
{
    public class PedidoProductoRepository : IPedidoProductoRepository
    {
        private readonly AppDbContext _context;

        public PedidoProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PedidoProducto> Add(PedidoProducto pedidoProducto)
        {
            _context.PedidoProductos.Add(pedidoProducto);
            await _context.SaveChangesAsync();
            return pedidoProducto;
        }

        public async Task<IEnumerable<PedidoProducto>> GetByPedidoId(int pedidoId)
        {
            return await _context.PedidoProductos
                .Where(pp => pp.PedidoId == pedidoId)
                .Include(pp => pp.Producto) // Incluye detalles del producto
                .ToListAsync();
        }

        public async Task<IEnumerable<PedidoProducto>> GetByProductoId(int productoId)
        {
            return await _context.PedidoProductos
                .Where(pp => pp.ProductoId == productoId)
                .Include(pp => pp.Pedido) // Incluye detalles del pedido
                .ToListAsync();
        }

        public async Task<bool> Delete(int pedidoProductoId)
        {
            var pedidoProducto = await _context.PedidoProductos.FindAsync(pedidoProductoId);
            if (pedidoProducto == null)
                return false;

            _context.PedidoProductos.Remove(pedidoProducto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
