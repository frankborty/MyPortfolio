using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data.Repositories.IncomeRepo
{
    public interface IIncomeRepo
    {
        Task<Income?> GetIncomeAsync(int incomeId);
        Task<IEnumerable<Income>> GetAllIncomesAsync();
        Task<Income> AddIncomeAsync(Income income);
        Task AddIncomeListAsync(List<Income> incomeList);
        Task DeleteIncomeAsync(int incomeId);
        Task<Income> UpdateIncomeAsync(int incomeId, Income income);
    }
}
