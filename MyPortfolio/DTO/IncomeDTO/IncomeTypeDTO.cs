using MyPortfolio.Models.Incomes;

namespace MyPortfolio.DTO.IncomeDTO
{
    public class IncomeTypeDTO
    { 

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public IncomeTypeDTO() { }

        public IncomeTypeDTO(IncomeType incomeType)
        {
            Id = incomeType.Id;
            Name = incomeType.Name;
        }

    }
}
