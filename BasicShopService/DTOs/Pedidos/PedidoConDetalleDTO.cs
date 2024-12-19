using Proyecto.Models;

namespace BasicShopService.DTOs.Pedidos
{
    public class PedidoConDetalleDTO
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int VendedorId { get; set; }
        public int DeliveryId { get; set; }
        public EstadoPedido Estado { get; set; }
        public List<PedidoProductoDetalleDTO> Productos { get; set; }
    }
}
