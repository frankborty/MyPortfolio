namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExpenseCategoryDTO Category { get; set; } = new ExpenseCategoryDTO();
        public ExpenseTypeDTO() { }
    }
}
