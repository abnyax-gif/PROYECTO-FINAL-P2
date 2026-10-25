using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("proveedores")]
public class Proveedor
{
    [Key]
    [Column("id_proveedor")]
    public int IdProveedor { get; set; }

    [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
    [StringLength(60)]
    [Column("proveedor")]
    public string NombreProveedor { get; set; } = string.Empty;

    [StringLength(12)]
    [Column("nit")]
    public string? Nit { get; set; }

    [StringLength(80)]
    [Column("direccion")]
    public string? Direccion { get; set; }

    [StringLength(25)]
    [Column("telefono")]
    public string? Telefono { get; set; }
}