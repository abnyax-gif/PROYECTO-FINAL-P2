using Microsoft.EntityFrameworkCore;
using PosSystem.Shared.Entities;

namespace PosSystem.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Compra> Compras => Set<Compra>();
public DbSet<CompraDetalle> ComprasDetalle => Set<CompraDetalle>();
public DbSet<Venta> Ventas => Set<Venta>();
public DbSet<VentaDetalle> VentasDetalle => Set<VentaDetalle>();
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Puesto> Puestos => Set<Puesto>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<MenuOpcion> MenuOpciones => Set<MenuOpcion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo jerárquico del Menú Dinámico
        modelBuilder.Entity<MenuOpcion>(e =>
        {
            e.HasOne(m => m.Padre)
             .WithMany(m => m.Hijos)
             .HasForeignKey(m => m.IdPadre)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}