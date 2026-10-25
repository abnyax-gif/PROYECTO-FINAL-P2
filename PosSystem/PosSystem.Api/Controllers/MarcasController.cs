using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MarcasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
    {
        return await _context.Marcas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Marca>> GetMarca(short id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null) return NotFound();
        return marca;
    }

    [HttpPost]
    public async Task<ActionResult<Marca>> PostMarca(Marca marca)
    {
        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMarca), new { id = marca.IdMarca }, marca);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMarca(short id, Marca marca)
    {
        if (id != marca.IdMarca) return BadRequest();
        _context.Entry(marca).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarca(short id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null) return NotFound();
        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}