using MyPortfolio.Models.Expenses;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public ExpenseTypeDTO ExpenseType { get; set; } = new ExpenseTypeDTO();

        public ExpenseDTO() { }
        public ExpenseDTO(Expense expense)
        {
            Id = expense.Id;
            Description = expense.Description;
            Amount = expense.Amount;
            TimeStamp = expense.TimeStamp;
            if (expense.ExpenseType is not null)
            {
                ExpenseType = new ExpenseTypeDTO(expense.ExpenseType);
            }
        }
    }
}
