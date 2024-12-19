using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public class ExpenseCategoryRepo : IExpenseCategoryRepo
    {
        private readonly DataDbContext dataDbContext;

        public ExpenseCategoryRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategorysAsync()
        {
            return await dataDbContext.ExpenseCategories.ToListAsync();
        }

        public async Task<ExpenseCategory?> GetExpenseCategoryByNameAsync(string expenseCategoryName)
        {
            return await dataDbContext.ExpenseCategories
                .Include(e => e.ExpenseTypes)
                .FirstOrDefaultAsync(e => e.Name == expenseCategoryName);
        }

        public async Task<ExpenseCategory?> GetExpenseCategoryAsync(int expenseCategoryId)
        {
            return await dataDbContext.ExpenseCategories
                .Include(e => e.ExpenseTypes)
                .FirstOrDefaultAsync(e => e.Id == expenseCategoryId);
        }

        public async Task<ExpenseCategory> UpdateExpenseCategory(int expenseCategoryId, ExpenseCategory expenseCategory)
        {
            ExpenseCategory? expenseCategoryToUpdate = await dataDbContext.ExpenseCategories
                .FirstOrDefaultAsync(e => e.Id == expenseCategoryId);

            if (expenseCategoryToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            expenseCategoryToUpdate.Name = expenseCategory.Name;
            await dataDbContext.SaveChangesAsync();
            return expenseCategoryToUpdate;
        }

        public async Task<ExpenseCategory> AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            var result = await dataDbContext.ExpenseCategories.AddAsync(expenseCategory);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddExpenseCategoryList(List<ExpenseCategory> expenseCategoryList)
        {
            await dataDbContext.ExpenseCategories.AddRangeAsync(expenseCategoryList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteExpenseCategory(int expenseCategoryId)
        {
            var expense = await dataDbContext.Expenses.FindAsync(expenseCategoryId);
            if (expense is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.Expenses.Remove(expense);
            await dataDbContext.SaveChangesAsync();
        }
    }
}
