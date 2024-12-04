using MyPortfolio.Models.Expenses;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public ExpenseTypeDTO ExpenseType { get; set; } = new ExpenseTypeDTO();

        public ExpenseDTO() { }
        public ExpenseDTO(Expense expense)
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
