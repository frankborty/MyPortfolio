using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Utility.IncomeUtils
{
    public class IncomeStaticUtils
    {
        public static Income CreateIncomeFromIncomeDto(IncomeDTO incomeToAdd)
        {
            var income = new Income()
            {
                Amount = incomeToAdd.Amount,
                Date = incomeToAdd.Date,
                Note = incomeToAdd.Note,
                TypeId = incomeToAdd.IncomeType.Id,
            };
            //income.TypeId = incomeToAdd.IncomeType.Id;
            return income;

        }


        public static async Task AddSingleIncome(IIncomeRepo _incomeRepo, Income incomeToAdd)
        {
            await _incomeRepo.AddIncomeAsync(incomeToAdd);
        }
    }
}
