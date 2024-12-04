using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data.Repositories.IncomeRepo
{
    public interface IIncomeTypeRepo
    {
        Task<IEnumerable<IncomeType>> GetAllIncomeTypesAsync();
        Task<IncomeType?> GetIncomeTypeAsync(int incomeTypeId);
        Task<IncomeType?> GetIncomeTypeByNameAsync(string incomeTypeName);
        Task<IncomeType> AddIncomeType(IncomeType incomeType);
        Task AddIncomeTypeList(List<IncomeType> incomeTypeList);
        Task<IncomeType> UpdateIncomeType(int incomeTypeList, IncomeType incomeType);
        Task<IncomeType> DeleteIncomeType(int incomeTypeList);
    }
}
