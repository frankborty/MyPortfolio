using MyPortfolio.Models.Expenses;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseToAddDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;

        public ExpenseToAddDTO() { }
        public ExpenseToAddDTO(Expense expense)
        {
            Description = expense.Description;
            Amount = expense.Amount;
            TimeStamp = expense.TimeStamp;
            if (expense.ExpenseType is not null)
            {
                ExpenseTypeId = expense.ExpenseType.Id;
                ExpenseTypeName = expense.ExpenseType.Name;
            }
        }
    }
}
