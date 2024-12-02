using MyPortfolio.Models.Expenses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseToAddDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;

        public ExpenseToAddDTO() { }
        public ExpenseToAddDTO(Expense expense)
        {
            Description = expense.Description;
            Amount = expense.Amount;
            Date = expense.Date;
            Note = expense.Note;
            if (expense.ExpenseType is not null)
            {
                ExpenseTypeId = expense.ExpenseType.Id;
                ExpenseTypeName = expense.ExpenseType.Name;
            }
        }
    }
}
