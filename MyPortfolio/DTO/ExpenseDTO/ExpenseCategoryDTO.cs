namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ExpenseCategoryDTO()
        {
            Id = -1;
        }
    }
}
