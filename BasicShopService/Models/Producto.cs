using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicShopService.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }

        [MaxLength(50)]
        public string Etiqueta { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UnidadStock { get; set; }

        public ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();

    }
}

