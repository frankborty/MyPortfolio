using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseTypeRepo
    {
        Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync();
        Task<ExpenseType?> GetExpenseTypeAsync(int expenseTypeId);
        Task<ExpenseType?> GetExpenseTypeByNameAsync(string expenseTypeName);
        Task<ExpenseType> AddExpenseType(ExpenseType expenseType);
        Task AddExpenseTypeList(List<ExpenseType> expenseTypeList);
        Task<ExpenseType> UpdateExpenseType(int expenseTypeList, ExpenseType expenseType);
        Task<ExpenseType> DeleteExpenseType(int expenseTypeList);
    }
}
