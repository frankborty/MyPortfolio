using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseTypeRepo
    {
        Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync();
        Task<ExpenseType?> GetExpenseTypeAsync(int expenseTypeId);
        Task<ExpenseType?> GetExpenseTypeByNameAsync(string expenseTypeName);
        Task<ExpenseType> AddExpenseTypeAsync(ExpenseType expenseType);
        Task AddExpenseTypeListAsync(List<ExpenseType> expenseTypeList);
        Task<ExpenseType> UpdateExpenseTypeAsync(int expenseTypeList, ExpenseType expenseType);
        Task DeleteExpenseTypeAsync(int expenseTypeList);
    }
}
