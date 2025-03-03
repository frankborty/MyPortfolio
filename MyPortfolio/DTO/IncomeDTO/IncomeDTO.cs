namespace MyPortfolio.DTO.IncomeDTO
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Note { get; set; } = string.Empty;
        public IncomeTypeDTO IncomeType { get; set; } = new IncomeTypeDTO();

        public IncomeDTO() { }
    }
}
