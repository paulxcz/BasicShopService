using BasicShopService.Repositories.Interfaces;
using BasicShopService.Services.Interfaces;
using Proyecto.Models;

namespace BasicShopService.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            return await _usuarioRepository.Add(usuario);
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            return await _usuarioRepository.Update(usuario);
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            return await _usuarioRepository.Delete(id);
        }

        public async Task<Usuario> Autenticar(string email, string password)
        {
            var usuario = await _usuarioRepository.GetByEmail(email);
            if (usuario == null || !Helpers.PasswordHasher.VerifyPassword(password, usuario.PasswordHash))
                return null;
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosDeliveryActivos()
        {
            return await _usuarioRepository.ObtenerUsuariosDeliveryActivos();
        }
    }
}
