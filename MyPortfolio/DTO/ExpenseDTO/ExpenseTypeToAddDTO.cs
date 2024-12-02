using MyPortfolio.Models.Expenses;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseTypeToAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public ExpenseTypeToAddDTO() { }

        public ExpenseTypeToAddDTO(ExpenseType expenseType)
        {
            Name = expenseType.Name;
            if (expenseType.Category is not null)
            {
                CategoryId = expenseType.Category.Id;
            }
        }
    }
}
