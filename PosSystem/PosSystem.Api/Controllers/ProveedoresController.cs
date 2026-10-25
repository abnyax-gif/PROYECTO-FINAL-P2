using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProveedoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
    {
        return await _context.Proveedores.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedor>> GetProveedor(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor == null) return NotFound();
        return proveedor;
    }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
    {
        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.IdProveedor }, proveedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
    {
        if (id != proveedor.IdProveedor) return BadRequest();
        _context.Entry(proveedor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProveedor(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor == null) return NotFound();
        _context.Proveedores.Remove(proveedor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}