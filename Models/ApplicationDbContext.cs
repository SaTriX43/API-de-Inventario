using Microsoft.EntityFrameworkCore;

namespace API_de_Inventario.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Movimientos)
                .WithOne(m => m.Producto)
                .HasForeignKey(m => m.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
