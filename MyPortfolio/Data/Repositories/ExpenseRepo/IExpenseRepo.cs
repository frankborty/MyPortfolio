using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.ExpenseRepo
{
    public interface IExpenseRepo
    {
        Task<Expense?> GetExpenseAsync(int expenseId);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> AddExpenseAsync(Expense expense);
        Task AddExpenseListAsync(List<Expense> expenseList);
        Task DeleteExpenseAsync(int expenseId);
        Task <Expense> UpdateExpenseAsync(int expenseId, Expense expense);
    }
}
