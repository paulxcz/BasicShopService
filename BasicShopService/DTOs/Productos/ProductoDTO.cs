namespace BasicShopService.DTOs.Productos
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Etiqueta { get; set; }
        public decimal Precio { get; set; }
        public int UnidadStock { get; set; }
    }
}
