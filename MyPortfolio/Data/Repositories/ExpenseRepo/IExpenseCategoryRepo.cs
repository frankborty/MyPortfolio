using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseCategoryRepo
    {
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategorysAsync();
        Task<ExpenseCategory?> GetExpenseCategoryAsync(int expenseCategoryId);
        Task<ExpenseCategory?> GetExpenseCategoryByNameAsync(string expenseCategoryName);
        Task<ExpenseCategory> AddExpenseCategoryAsync(ExpenseCategory expenseCategory);
        Task<ExpenseCategory> UpdateExpenseCategoryAsync(int expenseCategoryId, ExpenseCategory expenseCategory);
        Task DeleteExpenseCategoryAsync(int expenseCategoryId);
        Task AddExpenseCategoryListAsync(List<ExpenseCategory> expenseCategoryList);
    }
}
