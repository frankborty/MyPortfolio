namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseToAddDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        public string Note { get; set; } = string.Empty;
        public string ExpenseType { get; set; } = string.Empty;

        public ExpenseToAddDTO() { }
    }
}
