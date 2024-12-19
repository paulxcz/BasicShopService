using Proyecto.Models;

namespace BasicShopService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerarToken(Usuario usuario);
    }

}
