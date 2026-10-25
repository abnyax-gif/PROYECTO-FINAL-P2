using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [StringLength(50)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [Column("nombre_completo")]
    public string NombreCompleto { get; set; } = string.Empty;

    [Column("id_empleado")]
    public int? IdEmpleado { get; set; }

    [ForeignKey("IdEmpleado")]
    public Empleado? Empleado { get; set; }
}