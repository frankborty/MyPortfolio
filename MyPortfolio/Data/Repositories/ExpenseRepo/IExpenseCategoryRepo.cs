using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseCategoryRepo
    {
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategorysAsync();
        Task<ExpenseCategory?> GetExpenseCategoryAsync(int expenseCategoryId);
        Task<ExpenseCategory?> GetExpenseCategoryByNameAsync(string expenseCategoryName);
        Task<ExpenseCategory> AddExpenseCategory(ExpenseCategory expenseCategory);
        Task<ExpenseCategory> UpdateExpenseCategory(int expenseCategoryId, ExpenseCategory expenseCategory);
        Task<ExpenseCategory> DeleteExpenseCategory(int expenseCategoryId);
        Task AddExpenseCategoryList(List<ExpenseCategory> expenseCategoryList);
    }
}
