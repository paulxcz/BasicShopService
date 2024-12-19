using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CodigoTrabajador { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }

        [Required]
        public string Puesto { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }

        [Required]
        public EstadoUsuario Estado { get; set; }

        [Required]
        public string PasswordHash { get; set; } 
    }
    public enum RolUsuario
    {
        Encargado,
        Vendedor,
        Delivery
    }

    public enum EstadoUsuario
    {
        Activo,
        Inactivo
    }
}
