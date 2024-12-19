using Proyecto.Models;


namespace BasicShopService.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Add(Usuario usuario);
        Task<Usuario> GetById(int id);
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> Delete(int id);
        Task<IEnumerable<Usuario>> ObtenerUsuariosDeliveryActivos();


    }
}
