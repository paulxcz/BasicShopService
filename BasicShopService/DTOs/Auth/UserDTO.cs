namespace BasicShopService.DTOs.Auth
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string CodigoTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Puesto { get; set; }
        public string Rol { get; set; }
    }
}
