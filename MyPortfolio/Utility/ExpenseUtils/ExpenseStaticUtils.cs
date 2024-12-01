using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Utility.ExpenseUtils
{
    public class ExpenseStaticUtils
    {
        public static Expense CreateExpenseFromExpenseToAddDto(ExpenseToAddDTO expenseToAdd)
        {
            return new Expense()
            {
                Amount = expenseToAdd.Amount,
                Description = expenseToAdd.Description,
                TimeStamp = expenseToAdd.TimeStamp,
                TypeId = expenseToAdd.ExpenseTypeId
            };
        }

        public static async Task AddSingleExpense(IExpenseRepo _expenseRepo, Expense expenseToAdd)
        {
            await _expenseRepo.AddExpense(expenseToAdd);
        }
    }
}
