using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmpleadosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
    {
        return await _context.Empleados.Include(e => e.Puesto).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empleado>> GetEmpleado(int id)
    {
        var empleado = await _context.Empleados.Include(e => e.Puesto).FirstOrDefaultAsync(e => e.IdEmpleado == id);
        if (empleado == null) return NotFound();
        return empleado;
    }

    [HttpPost]
    public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
    {
        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.IdEmpleado }, empleado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
    {
        if (id != empleado.IdEmpleado) return BadRequest();
        _context.Entry(empleado).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpleado(int id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado == null) return NotFound();
        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}