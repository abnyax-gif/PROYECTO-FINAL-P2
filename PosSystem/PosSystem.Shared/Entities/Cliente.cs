using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("clientes")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(60)]
    [Column("nombres")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(60)]
    [Column("apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    [StringLength(12)]
    [Column("nit")]
    public string? Nit { get; set; }

    [Column("genero")]
    public bool Genero { get; set; }

    [StringLength(25)]
    [Column("telefono")]
    public string? Telefono { get; set; }

    [StringLength(45)]
    [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
    [Column("correo_electronico")]
    public string? CorreoElectronico { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;
}