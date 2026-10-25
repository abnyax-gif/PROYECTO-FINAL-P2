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
public class ComprasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ComprasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
    {
        return await _context.Compras
            .Include(c => c.Proveedor)
            .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
            .OrderByDescending(c => c.FechaIngreso)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarCompra([FromBody] CompraCreateDTO dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var compra = new Compra
            {
                NoOrdenCompra = dto.NoOrdenCompra,
                IdProveedor = dto.IdProveedor,
                FechaOrden = dto.FechaOrden,
                FechaIngreso = DateTime.Now
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            foreach (var item in dto.Detalles)
            {
                var producto = await _context.Productos.FindAsync(item.IdProducto);
                if (producto == null)
                {
                    await transaction.RollbackAsync();
                    return BadRequest($"El producto con ID {item.IdProducto} no existe.");
                }

                // REGLAS DE NEGOCIO DE COMPRA:
                // 1. Aumentar existencia automáticamente
                producto.Existencia += item.Cantidad;

                // 2. Actualizar precio de costo con el de la compra
                producto.PrecioCosto = item.PrecioCostoUnitario;

                // 3. Calcular precio de venta con un 25% de margen
                producto.PrecioVenta = Math.Round(item.PrecioCostoUnitario * 1.25m, 2);

                var detalle = new CompraDetalle
                {
                    IdCompra = compra.IdCompra,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioCostoUnitario = item.PrecioCostoUnitario
                };
                _context.ComprasDetalle.Add(detalle);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { Mensaje = "Compra registrada con éxito y existencias/precios actualizados." });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"Error al procesar la compra: {ex.Message}");
        }
    }
}