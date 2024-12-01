using MyPortfolio.Models.Expenses;

namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseTypeDTO
    { 

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExpenseCategoryDTO Category { get; set; } = new ExpenseCategoryDTO();

        public ExpenseTypeDTO() { }

        public ExpenseTypeDTO(ExpenseType expenseType)
        {
            Id = expenseType.Id;
            Name = expenseType.Name;
            if (expenseType.Category is not null)
            {
                Category = new ExpenseCategoryDTO(expenseType.Category);
            }
        }

    }
}
