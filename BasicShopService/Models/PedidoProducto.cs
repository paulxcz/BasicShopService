using Proyecto.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasicShopService.Models
{
    public class PedidoProducto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        // Propiedades de navegación
        public Pedido Pedido { get; set; }

        public Producto Producto { get; set; }
    }
}
