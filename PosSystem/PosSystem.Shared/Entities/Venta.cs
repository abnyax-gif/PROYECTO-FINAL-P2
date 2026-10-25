using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("ventas")]
public class Venta
{
    [Key]
    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Required]
    [Column("no_factura")]
    public int NoFactura { get; set; }

    [Required]
    [StringLength(1)]
    [Column("serie")]
    public string Serie { get; set; } = "A";

    [Column("fecha_factura")]
    public DateTime FechaFactura { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("id_empleado")]
    public int IdEmpleado { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }

    [ForeignKey("IdEmpleado")]
    public Empleado? Empleado { get; set; }

    public List<VentaDetalle> Detalles { get; set; } = new();
}

[Table("ventas_detalle")]
public class VentaDetalle
{
    [Key]
    [Column("id_venta_detalle")]
    public long IdVentaDetalle { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario", TypeName = "decimal(8,2)")]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("IdVenta")]
    public Venta? Venta { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}