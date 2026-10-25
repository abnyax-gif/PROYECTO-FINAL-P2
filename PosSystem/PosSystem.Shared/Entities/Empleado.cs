using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("empleados")]
public class Empleado
{
    [Key]
    [Column("id_empleado")]
    public int IdEmpleado { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(60)]
    [Column("nombres")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(60)]
    [Column("apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    [StringLength(80)]
    [Column("direccion")]
    public string? Direccion { get; set; }

    [StringLength(25)]
    [Column("telefono")]
    public string? Telefono { get; set; }

    [StringLength(15)]
    [Column("dpi")]
    public string? Dpi { get; set; }

    [Column("genero")]
    public bool Genero { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un puesto")]
    [Column("id_puesto")]
    public short IdPuesto { get; set; }

    [Column("fecha_inicio_labores")]
    public DateTime? FechaInicioLabores { get; set; }

    [Column("fecha_ingreso")]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [ForeignKey("IdPuesto")]
    public Puesto? Puesto { get; set; }
}