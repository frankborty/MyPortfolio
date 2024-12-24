namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseTypeToAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public ExpenseTypeToAddDTO() { }
    }
}
