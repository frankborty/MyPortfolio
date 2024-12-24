using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data.Repositories.IncomeRepo
{
    public interface IIncomeTypeRepo
    {
        Task<IEnumerable<IncomeType>> GetAllIncomeTypesAsync();
        Task<IncomeType?> GetIncomeTypeAsync(int incomeTypeId);
        Task<IncomeType?> GetIncomeTypeByNameAsync(string incomeTypeName);
        Task<IncomeType> AddIncomeTypeAsync(IncomeType incomeType);
        Task AddIncomeTypeListAsync(List<IncomeType> incomeTypeList);
        Task<IncomeType> UpdateIncomeTypeAsync(int incomeTypeList, IncomeType incomeType);
        Task DeleteIncomeTypeAsync(int incomeTypeList);
    }
}
