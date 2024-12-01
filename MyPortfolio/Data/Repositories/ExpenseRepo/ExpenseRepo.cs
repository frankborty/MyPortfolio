using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Expenses;
using System;

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
                .ThenInclude(et => et.Category)
                .FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await dataDbContext.Expenses
                .Include(e => e.ExpenseType)
                .ThenInclude(et => et.Category)
                .ToListAsync();
        }

        public async Task<Expense> AddExpense(Expense expense)
        {
            var result = await dataDbContext.Expenses.AddAsync(expense);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ExpenseType?> GetExpenseTypeAsync(int expenseTypeId)
        {
            return await dataDbContext.ExpenseTypes.FirstOrDefaultAsync(e => e.Id == expenseTypeId);
        }

        public async Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync()
        {
            return await dataDbContext.ExpenseTypes.ToListAsync();
        }

        public async Task<ExpenseType> AddExpenseType(ExpenseType expenseType)
        {
            var result = await dataDbContext.ExpenseTypes.AddAsync(expenseType);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ExpenseCategory?> GetExpenseCategoryAsync(int expenseCategoryId)
        {
            return await dataDbContext.ExpenseCategories.FirstOrDefaultAsync(e => e.Id == expenseCategoryId);
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategorysAsync()
        {
            return await dataDbContext.ExpenseCategories.ToListAsync();
        }

        public async Task<ExpenseCategory> AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            var result = await dataDbContext.ExpenseCategories.AddAsync(expenseCategory);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ExpenseCategory?> GetExpenseCategoryByNameAsync(string expenseCategoryName)
        {
            return await dataDbContext.ExpenseCategories
                .Include(e => e.ExpenseTypes)
                .FirstOrDefaultAsync(e => e.Name == expenseCategoryName);
        }

        public async Task AddExpenseTypeList(List<ExpenseType> expenseTypeList)
        {
            await dataDbContext.ExpenseTypes.AddRangeAsync(expenseTypeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteExpense(int expenseId)
        {
            var expense = await dataDbContext.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                dataDbContext.Expenses.Remove(expense); 
                await dataDbContext.SaveChangesAsync(); 
            }
            throw new KeyNotFoundException();
        }

        public async Task<Expense> UpdateExpense(int expenseId, Expense expense)
        {
            Expense? expenseToUpdate = await dataDbContext.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId);

            if(expenseToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            expenseToUpdate.Description = expense.Description;
            expenseToUpdate.Amount = expense.Amount;
            expenseToUpdate.TimeStamp = expense.TimeStamp;
            expenseToUpdate.TypeId = expense.TypeId;
            await dataDbContext.SaveChangesAsync();
            return expenseToUpdate;
        }
    }
}
