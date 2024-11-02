using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Orden> Ordenes { get; set; }
    public DbSet<OrdenDetalle> OrdenDetalles { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la propiedad Precio de Producto
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");

        // Configuración de la propiedad Total de Orden
        modelBuilder.Entity<Orden>()
            .Property(o => o.Total)
            .HasColumnType("decimal(18,2)");

        
        // Relación entre OrdenDetalle y Producto
        modelBuilder.Entity<OrdenDetalle>()
            .HasOne(od => od.Producto)
            .WithMany()
            .HasForeignKey(od => od.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relación entre OrdenDetalle y Orden
        modelBuilder.Entity<OrdenDetalle>()
            .HasOne(od => od.Orden)
            .WithMany(o => o.OrdenDetalles)
            .HasForeignKey(od => od.OrdenId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración para calcular Subtotal en OrdenDetalle (puedes calcular esto en el código o en la vista)
        modelBuilder.Entity<OrdenDetalle>()
            .Property(od => od.Precio)
            .HasColumnType("decimal(18,2)");
    }
}
