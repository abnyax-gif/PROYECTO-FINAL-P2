using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosSystem.Api.Data;
using PosSystem.Shared.DTOs;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VentasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
    {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Empleado)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .OrderByDescending(v => v.FechaIngreso)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarVenta([FromBody] VentaCreateDTO dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // REGLA DE NEGOCIO: Verificar stock suficiente ANTES de procesar
            foreach (var item in dto.Detalles)
            {
                var prod = await _context.Productos.FindAsync(item.IdProducto);
                if (prod == null)
                {
                    await transaction.RollbackAsync();
                    return BadRequest($"El producto ID {item.IdProducto} no existe.");
                }

                if (item.Cantidad > prod.Existencia)
                {
                    await transaction.RollbackAsync();
                    return BadRequest($"No se puede realizar la venta. Existencia insuficiente para '{prod.NombreProducto}'. Disponible: {prod.Existencia}, Solicitado: {item.Cantidad}.");
                }
            }

            var venta = new Venta
            {
                NoFactura = dto.NoFactura,
                Serie = dto.Serie,
                FechaFactura = dto.FechaFactura,
                IdCliente = dto.IdCliente,
                IdEmpleado = dto.IdEmpleado,
                FechaIngreso = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            foreach (var item in dto.Detalles)
            {
                var prod = await _context.Productos.FindAsync(item.IdProducto)!;

                // REGLA DE NEGOCIO: Disminuir la existencia automáticamente
                prod!.Existencia -= item.Cantidad;

                var detalle = new VentaDetalle
                {
                    IdVenta = venta.IdVenta,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = prod.PrecioVenta // Usa el precio_venta actual del producto
                };
                _context.VentasDetalle.Add(detalle);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { Mensaje = "Venta registrada con éxito y existencias reducidas." });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"Error al procesar la venta: {ex.Message}");
        }
    }
}