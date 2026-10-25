using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReportesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("productos")]
    public async Task<IActionResult> GetReporteProductos()
    {
        var productos = await _context.Productos
            .Include(p => p.Marca)
            .Select(p => new
            {
                p.IdProducto,
                Producto = p.NombreProducto,
                Marca = p.Marca != null ? p.Marca.NombreMarca : "Sin Marca",
                p.Existencia,
                p.PrecioCosto,
                p.PrecioVenta
            })
            .ToListAsync();

        return Ok(productos);
    }

    [HttpGet("existencias")]
    public async Task<IActionResult> GetReporteExistencias()
    {
        var existencias = await _context.Productos
            .Include(p => p.Marca)
            .Where(p => p.Existencia <= 5) // Alerta de stock bajo
            .Select(p => new
            {
                p.IdProducto,
                Producto = p.NombreProducto,
                Marca = p.Marca != null ? p.Marca.NombreMarca : "Sin Marca",
                p.Existencia
            })
            .ToListAsync();

        return Ok(existencias);
    }

    [HttpGet("ventas")]
    public async Task<IActionResult> GetReporteVentas()
    {
        var ventas = await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Empleado)
            .Include(v => v.Detalles)
            .Select(v => new
            {
                v.FechaFactura,
                Cliente = $"{v.Cliente!.Nombres} {v.Cliente.Apellidos}",
                Empleado = $"{v.Empleado!.Nombres} {v.Empleado.Apellidos}",
                TotalVenta = v.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario)
            })
            .ToListAsync();

        return Ok(ventas);
    }
}