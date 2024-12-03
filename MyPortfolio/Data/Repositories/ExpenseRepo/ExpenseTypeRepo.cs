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

        public async Task<ExpenseType> AddExpenseType(ExpenseType expenseType)
        {
            var result = await dataDbContext.ExpenseTypes.AddAsync(expenseType);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddExpenseTypeList(List<ExpenseType> expenseTypeList)
        {
            await dataDbContext.ExpenseTypes.AddRangeAsync(expenseTypeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task<ExpenseType> DeleteExpenseType(int expenseTypeId)
        {
            var expense = await dataDbContext.ExpenseTypes.FindAsync(expenseTypeId);
            if (expense != null)
            {
                dataDbContext.ExpenseTypes.Remove(expense);
                await dataDbContext.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
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

        public async Task<ExpenseType> UpdateExpenseType(int expenseTypeId, ExpenseType expenseType)
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
