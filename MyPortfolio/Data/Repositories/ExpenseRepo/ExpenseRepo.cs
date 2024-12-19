using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public class ExpenseRepo : IExpenseRepo
    {
        private readonly DataDbContext dataDbContext;

        public ExpenseRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<Expense?> GetExpenseAsync(int expenseId)
        {
            return await dataDbContext.Expenses
                .Include(e => e.ExpenseType)
                .ThenInclude(et => et!.Category)
                .FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await dataDbContext.Expenses
                .Include(e => e.ExpenseType)
                .ThenInclude(et => et!.Category)
                .ToListAsync();
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            var result = await dataDbContext.Expenses.AddAsync(expense);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteExpenseAsync(int expenseId)
        {
            var expense = await dataDbContext.Expenses.FindAsync(expenseId);
            if (expense is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.Expenses.Remove(expense);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<Expense> UpdateExpenseAsync(int expenseId, Expense expense)
        {
            Expense? expenseToUpdate = await dataDbContext.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId);

            if (expenseToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            expenseToUpdate.Description = expense.Description;
            expenseToUpdate.Amount = expense.Amount;
            expenseToUpdate.Date = expense.Date;
            expenseToUpdate.TypeId = expense.TypeId;
            await dataDbContext.SaveChangesAsync();
            return expenseToUpdate;
        }

        public async Task AddExpenseListAsync(List<Expense> expenseList)
        {
            await dataDbContext.Expenses.AddRangeAsync(expenseList);
            await dataDbContext.SaveChangesAsync();
            return;
        }
    }
}
