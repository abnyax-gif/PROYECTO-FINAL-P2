using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("puestos")]
public class Puesto
{
    [Key]
    [Column("id_puesto")]
    public short IdPuesto { get; set; }

    [Required(ErrorMessage = "El nombre del puesto es obligatorio")]
    [StringLength(50)]
    [Column("puesto")]
    public string NombrePuesto { get; set; } = string.Empty;
}