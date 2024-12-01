using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseRepo
    {
        Task<Expense?> GetExpenseAsync(int expenseId);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> AddExpense(Expense expense);
        Task DeleteExpense(int expenseId);
        Task <Expense> UpdateExpense(int expenseId, Expense expense);

        Task<ExpenseType?> GetExpenseTypeAsync(int expenseTypeId);
        Task<IEnumerable<ExpenseType>> GetAllExpenseTypesAsync();
        Task<ExpenseType> AddExpenseType(ExpenseType expenseType);
        Task AddExpenseTypeList(List<ExpenseType> expenseTypeList);

        Task<ExpenseCategory?> GetExpenseCategoryAsync(int expenseCategoryId);
        Task<ExpenseCategory?> GetExpenseCategoryByNameAsync(string expenseCategoryName);
        Task<IEnumerable<ExpenseCategory>> GetAllExpenseCategorysAsync();
        Task<ExpenseCategory> AddExpenseCategory(ExpenseCategory expenseCategory);
    }
}
