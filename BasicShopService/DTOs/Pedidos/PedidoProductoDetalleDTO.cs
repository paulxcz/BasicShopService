namespace BasicShopService.DTOs.Pedidos
{
    public class PedidoProductoDetalleDTO
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
    }
}
