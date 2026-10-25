using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystem.Shared.Entities;

[Table("marcas")]
public class Marca
{
    [Key]
    [Column("id_marca")]
    public short IdMarca { get; set; }

    [Required(ErrorMessage = "El nombre de la marca es obligatorio")]
    [StringLength(50)]
    [Column("marca")]
    public string NombreMarca { get; set; } = string.Empty;
}