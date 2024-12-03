using MyPortfolio.Models.Incomes;

namespace MyPortfolio.DTO.IncomeDTO
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public IncomeTypeDTO IncomeType { get; set; } = new IncomeTypeDTO();

        public IncomeDTO() { }
        public IncomeDTO(Income income)
        {
            Id = income.Id;
            Amount = income.Amount;
            Date = income.Date;
            Note = income.Note;
            if (income.IncomeType is not null)
            {
                IncomeType = new IncomeTypeDTO(income.IncomeType);
            }
        }
    }
}
