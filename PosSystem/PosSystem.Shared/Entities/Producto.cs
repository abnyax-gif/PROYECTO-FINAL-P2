using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("productos")]
public class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(50)]
    [Column("producto")]
    public string NombreProducto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar una marca")]
    [Column("id_marca")]
    public short IdMarca { get; set; }

    [StringLength(100)]
    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [StringLength(255)]
    [Column("imagen")]
    public string? Imagen { get; set; }

    [Column("precio_costo", TypeName = "decimal(8,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de costo debe ser mayor o igual a 0")]
    public decimal PrecioCosto { get; set; }

    [Column("precio_venta", TypeName = "decimal(8,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a 0")]
    public decimal PrecioVenta { get; set; }

    [Column("existencia")]
    [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa")]
    public int Existencia { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [ForeignKey("IdMarca")]
    public Marca? Marca { get; set; }
}