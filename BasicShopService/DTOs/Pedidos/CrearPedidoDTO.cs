using BasicShopService.Models;
using Proyecto.Models;

namespace BasicShopService.DTOs.Pedidos
{
    public class CrearPedidoDTO
    {
        public string NumeroPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int VendedorId { get; set; }
        public int DeliveryId { get; set; }
        public EstadoPedido Estado { get; set; }

        public List<CrearPedidoProductoDTO> Productos { get; set; }
    }

    public class CrearPedidoProductoDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
