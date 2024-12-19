using Proyecto.Models;

namespace BasicShopService.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
        Task<Usuario> ActualizarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(int id);
        Task<Usuario> Autenticar(string email, string password);
        Task<IEnumerable<Usuario>> ObtenerUsuariosDeliveryActivos();

    }
}
