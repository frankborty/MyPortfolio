namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseToAddDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Note { get; set; } = string.Empty;
        public string ExpenseType { get; set; } = string.Empty;

        public ExpenseToAddDTO() { }
    }
}
