using BasicShopService.Data;
using BasicShopService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using System;

namespace BasicShopService.Repositories.Implementations
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> Add(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> GetById(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProductos)
                .ThenInclude(pp => pp.Producto) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> FindByNumeroPedido(string numeroPedido)
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProductos)
                .ThenInclude(pp => pp.Producto) 
                .Where(p => p.NumeroPedido.Contains(numeroPedido))
                .ToListAsync();
        }

        public async Task<Pedido> Update(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> Delete(int id)
        {
            var pedido = await GetById(id);
            if (pedido == null) return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Pedido> UpdateEstado(int id, EstadoPedido nuevoEstado)
        {
            var pedido = await GetById(id);
            if (pedido == null)
            {
                throw new Exception("Pedido no encontrado");
            }
            pedido.Estado = nuevoEstado;
            // Código para actualizar el pedido en la base de datos
            return pedido;
        }
    }
}
