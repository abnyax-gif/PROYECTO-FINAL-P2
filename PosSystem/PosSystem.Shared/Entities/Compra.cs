using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("compras")]
public class Compra
{
    [Key]
    [Column("id_compra")]
    public int IdCompra { get; set; }

    [Required]
    [Column("no_orden_compra")]
    public int NoOrdenCompra { get; set; }

    [Required]
    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Column("fecha_orden")]
    public DateTime FechaOrden { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [ForeignKey("IdProveedor")]
    public Proveedor? Proveedor { get; set; }

    public List<CompraDetalle> Detalles { get; set; } = new();
}

[Table("compras_detalle")]
public class CompraDetalle
{
    [Key]
    [Column("id_compra_detalle")]
    public long IdCompraDetalle { get; set; }

    [Column("id_compra")]
    public int IdCompra { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_costo_unitario", TypeName = "decimal(8,2)")]
    public decimal PrecioCostoUnitario { get; set; }

    [ForeignKey("IdCompra")]
    public Compra? Compra { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}