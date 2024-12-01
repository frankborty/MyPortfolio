using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.ExpenseType)
                .WithMany(t => t.Expenses)
                .HasForeignKey(e => e.TypeId);


            modelBuilder.Entity<ExpenseType>()
                .HasOne(t => t.Category)
                .WithMany(c => c.ExpenseTypes)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
