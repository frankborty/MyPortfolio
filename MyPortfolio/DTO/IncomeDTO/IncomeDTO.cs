using MyPortfolio.Models.Incomes;

namespace MyPortfolio.DTO.IncomeDTO
{
    public class IncomeDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public IncomeTypeDTO ExpenseType { get; set; } = new IncomeTypeDTO();

        public IncomeDTO() { }
        public IncomeDTO(Income income)
        {
            Id = income.Id;
            Amount = income.Amount;
            Date = income.Date;
            Note = income.Note;
            if (income.IncomeType is not null)
            {
                ExpenseType = new IncomeTypeDTO(income.IncomeType);
            }
        }
    }
}
