namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        public string Note { get; set; } = string.Empty;
        public ExpenseTypeDTO ExpenseType { get; set; } = new ExpenseTypeDTO();

        public ExpenseDTO() { }
    }
}
