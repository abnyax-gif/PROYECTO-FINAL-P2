using System.ComponentModel.DataAnnotations;

namespace PosSystem.Shared.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDTO
{
    public bool Exito { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
}