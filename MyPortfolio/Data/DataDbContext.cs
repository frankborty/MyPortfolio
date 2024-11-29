using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyPortfolio.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<SottoCategoria> SottoCategorie { get; set; }
        public DbSet<Spesa> Spese { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        // Opzionale: Puoi configurare ulteriormente le relazioni nel metodo OnModelCreating se necessario.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazioni per le relazioni e le chiavi primarie, se necessarie
            modelBuilder.Entity<SottoCategoria>()
                .HasOne(s => s.Categoria)
                .WithMany(c => c.SottoCategorie)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);  // Configurazione della cancellazione in cascata
        }
    }
}