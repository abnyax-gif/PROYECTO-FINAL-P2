using System.ComponentModel.DataAnnotations;

namespace PosSystem.Shared.DTOs;

// --- DTOs PARA COMPRAS ---
public class CompraCreateDTO
{
    [Required(ErrorMessage = "El número de orden es obligatorio")]
    public int NoOrdenCompra { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un proveedor")]
    public int IdProveedor { get; set; }

    public DateTime FechaOrden { get; set; } = DateTime.Now;

    [MinLength(1, ErrorMessage = "Debe agregar al menos un producto a la compra")]
    public List<CompraDetalleDTO> Detalles { get; set; } = new();
}

public class CompraDetalleDTO
{
    [Required]
    public int IdProducto { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio de costo debe ser mayor a 0")]
    public decimal PrecioCostoUnitario { get; set; }
}

// --- DTOs PARA VENTAS ---
public class VentaCreateDTO
{
    [Required(ErrorMessage = "El número de factura es obligatorio")]
    public int NoFactura { get; set; }

    [Required(ErrorMessage = "La serie es obligatoria")]
    [StringLength(1)]
    public string Serie { get; set; } = "A";

    public DateTime FechaFactura { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Debe seleccionar un cliente")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un empleado")]
    public int IdEmpleado { get; set; }

    [MinLength(1, ErrorMessage = "Debe agregar al menos un producto a la venta")]
    public List<VentaDetalleDTO> Detalles { get; set; } = new();
}

public class VentaDetalleDTO
{
    [Required]
    public int IdProducto { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }
}