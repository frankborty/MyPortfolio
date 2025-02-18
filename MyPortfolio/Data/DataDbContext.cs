using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<AssetOperation> AssetOperations { get; set; }
        public DbSet<AssetValue> AssetValues { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.Date) // Assicurati di usare il nome aggiornato
                .HasColumnType("date"); // Specifica il tipo "date" per PostgreSQL

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.ExpenseType)
                .WithMany(t => t.Expenses)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExpenseType>()
                .HasOne(t => t.Category)
                .WithMany(c => c.ExpenseTypes)
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<Income>()
                .Property(e => e.Date) // Assicurati di usare il nome aggiornato
                .HasColumnType("date"); // Specifica il tipo "date" per PostgreSQL

            modelBuilder.Entity<Income>()
                .HasOne(i => i.IncomeType)
                .WithMany(t => t.Incomes)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asset>()
                .HasOne(e => e.AssetCategory)
                .WithMany(t => t.Assets)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<AssetOperation>()
                .HasOne(e => e.Asset)
                .WithMany(t => t.OperationList)
                .HasForeignKey(e => e.AssetId);

            modelBuilder.Entity<AssetValue>()
                .HasOne(e => e.Asset)
                .WithMany(t => t.ValueList)
                .HasForeignKey(e => e.AssetId);
        }
    }
}
