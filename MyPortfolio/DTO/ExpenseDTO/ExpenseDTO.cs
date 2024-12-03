using MyPortfolio.Models.Expenses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public IncomeTypeDTO ExpenseType { get; set; } = new ExpenseTypeDTO();

        public IncomeDTO() { }
        public IncomeDTO(Expense expense)
        {
            Id = expense.Id;
            Description = expense.Description;
            Amount = expense.Amount;
            Date = expense.Date;
            Note = expense.Note;
            if (expense.ExpenseType is not null)
            {
                ExpenseType = new ExpenseTypeDTO(expense.ExpenseType);
            }
        }
    }
}
