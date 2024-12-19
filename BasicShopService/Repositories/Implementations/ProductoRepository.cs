using BasicShopService.Data;
using BasicShopService.Models;
using BasicShopService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BasicShopService.Repositories.Implementations
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> Add(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> GetById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<IEnumerable<Producto>> FindByCriteria(string criterio)
        {
            return await _context.Productos
                .Where(p => p.SKU.Contains(criterio) || p.Nombre.Contains(criterio))
                .ToListAsync();
        }

        public async Task<Producto> Update(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> Delete(int id)
        {
            var producto = await GetById(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
