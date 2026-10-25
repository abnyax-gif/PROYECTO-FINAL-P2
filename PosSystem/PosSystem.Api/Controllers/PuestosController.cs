using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PuestosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PuestosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Puesto>>> GetPuestos()
    {
        return await _context.Puestos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Puesto>> GetPuesto(short id)
    {
        var puesto = await _context.Puestos.FindAsync(id);
        if (puesto == null) return NotFound("Puesto no encontrado");
        return puesto;
    }

    [HttpPost]
    public async Task<ActionResult<Puesto>> PostPuesto(Puesto puesto)
    {
        _context.Puestos.Add(puesto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPuesto), new { id = puesto.IdPuesto }, puesto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPuesto(short id, Puesto puesto)
    {
        if (id != puesto.IdPuesto) return BadRequest("ID no coincide");
        _context.Entry(puesto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePuesto(short id)
    {
        var puesto = await _context.Puestos.FindAsync(id);
        if (puesto == null) return NotFound();
        _context.Puestos.Remove(puesto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}