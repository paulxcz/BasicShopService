using BasicShopService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string NumeroPedido { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; }

        public DateTime? FechaDespacho { get; set; }

        public DateTime? FechaEntrega { get; set; }

        [Required]
        public int VendedorId { get; set; }

        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public EstadoPedido Estado { get; set; }

        public ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();
    }
    public enum EstadoPedido
    {
        PorAtender,
        EnProceso,
        Entregado
    }
}
