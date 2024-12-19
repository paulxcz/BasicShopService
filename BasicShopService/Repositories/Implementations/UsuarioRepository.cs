using BasicShopService.Data;
using BasicShopService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace BasicShopService.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await GetById(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosDeliveryActivos()
        {
            return await _context.Usuarios
                .Where(u => u.Rol == RolUsuario.Delivery && u.Estado == EstadoUsuario.Activo)
                .ToListAsync();
        }

    }
}
