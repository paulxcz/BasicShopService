using Proyecto.Models;

namespace BasicShopService.DTOs.Usuarios
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string CodigoTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Puesto { get; set; }
        public RolUsuario Rol { get; set; }
        public EstadoUsuario Estado { get; set; }
    }
}
