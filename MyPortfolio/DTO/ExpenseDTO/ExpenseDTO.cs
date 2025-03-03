namespace MyPortfolio.DTO.ExpenseDTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Note { get; set; } = string.Empty;
        public ExpenseTypeDTO ExpenseType { get; set; } = new ExpenseTypeDTO();

        public ExpenseDTO() { }
    }
}
