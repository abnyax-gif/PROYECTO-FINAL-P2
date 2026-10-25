using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;

namespace PosSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var menu = await _context.MenuOpciones
            .AsNoTracking()
            .Where(m => m.IdPadre == null)
            .OrderBy(m => m.Orden)
            .Select(m => new
            {
                m.IdMenu,
                m.Titulo,
                m.Ruta,
                m.Icono,
                m.Orden,
                Hijos = m.Hijos
                    .OrderBy(h => h.Orden)
                    .Select(h => new
                    {
                        h.IdMenu,
                        h.Titulo,
                        h.Ruta,
                        h.Icono,
                        h.IdPadre,
                        h.Orden
                    })
                    .ToList()
            })
            .ToListAsync();

        return Ok(menu);
    }
}