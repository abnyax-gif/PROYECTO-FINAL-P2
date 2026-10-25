using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Importante incluir esta directiva

namespace PosSystem.Shared.Entities;

[Table("menu_opciones")]
public class MenuOpcion
{
    [Key]
    [Column("id_menu")]
    public int IdMenu { get; set; }

    [Required]
    [StringLength(50)]
    [Column("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(100)]
    [Column("ruta")]
    public string? Ruta { get; set; }

    [StringLength(50)]
    [Column("icono")]
    public string? Icono { get; set; }

    [Column("id_padre")]
    public int? IdPadre { get; set; }

    [Column("orden")]
    public int Orden { get; set; }

    [ForeignKey("IdPadre")]
    [JsonIgnore] // Rompe la referencia circular al serializar a JSON
    public MenuOpcion? Padre { get; set; }

    public List<MenuOpcion> Hijos { get; set; } = new();
}