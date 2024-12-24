using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public class ExpenseTypeRepo : IExpenseTypeRepo
    {
        private readonly DataDbContext dataDbContext;

        public ExpenseTypeRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<ExpenseType> AddExpenseTypeAsync(ExpenseType expenseType)
        {
            var result = await dataDbContext.ExpenseTypes.AddAsync(expenseType);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddExpenseTypeListAsync(List<ExpenseType> expenseTypeList)
        {
            await dataDbContext.ExpenseTypes.AddRangeAsync(expenseTypeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteExpenseTypeAsync(int expenseTypeId)
        {
            var expense = await dataDbContext.ExpenseTypes.FindAsync(expenseTypeId);
            if (expense is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.ExpenseTypes.Remove(expense);
            await dataDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync()
        {
            return await dataDbContext.ExpenseTypes.ToListAsync();
        }

        public async Task<ExpenseType?> GetExpenseTypeAsync(int expenseTypeId)
        {
            return await dataDbContext.ExpenseTypes.FirstOrDefaultAsync(e => e.Id == expenseTypeId);
        }

        public async Task<ExpenseType?> GetExpenseTypeByNameAsync(string expenseTypeName)
        {
            return await dataDbContext.ExpenseTypes.FirstOrDefaultAsync(e => e.Name == expenseTypeName);
        }

        public async Task<ExpenseType> UpdateExpenseTypeAsync(int expenseTypeId, ExpenseType expenseType)
        {
            ExpenseType? expenseTypeToUpdate = await dataDbContext.ExpenseTypes
                .FirstOrDefaultAsync(e => e.Id == expenseTypeId);

            if (expenseTypeToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            expenseTypeToUpdate.Name = expenseType.Name;
            expenseTypeToUpdate.Category = expenseType.Category;
            await dataDbContext.SaveChangesAsync();
            return expenseTypeToUpdate;
        }
    }
}
