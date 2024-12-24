using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Utility.IncomeUtils
{
    public static class IncomeDTOConverter
    {
        public static IncomeDTO ToIncomeDTO(Income income)
        {
            return new IncomeDTO()
            {
                Id = income.Id,
                Amount = income.Amount,
                Date = income.Date,
                Note = income.Note,
                IncomeType = IncomeTypeDTOConverter.ToIncomeTypeDTO(income.IncomeType)
            };
        }

        public static Income FromIncomeDTO(IncomeDTO incomeToAdd)
        {
            return new Income()
            {
                Amount = incomeToAdd.Amount,
                Date = incomeToAdd.Date,
                Note = incomeToAdd.Note,
                TypeId = incomeToAdd.IncomeType.Id,
            };
        }
    }
    public static class IncomeTypeDTOConverter
    {
        public static IncomeTypeDTO ToIncomeTypeDTO(IncomeType? incomeType)
        {
            if (incomeType is null)
            {
                return new IncomeTypeDTO();
            }
            return new IncomeTypeDTO()
            {
                Id = incomeType.Id,
                Name = incomeType.Name
            };
        }
    }
}
