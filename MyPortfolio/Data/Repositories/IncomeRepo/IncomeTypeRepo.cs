using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data.Repositories.IncomeRepo
{
    public class IncomeTypeRepo : IIncomeTypeRepo
    {
        private readonly DataDbContext dataDbContext;

        public IncomeTypeRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<IncomeType> AddIncomeType(IncomeType incomeType)
        {
            var result = await dataDbContext.IncomeTypes.AddAsync(incomeType);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddIncomeTypeList(List<IncomeType> incomeTypeList)
        {
            await dataDbContext.IncomeTypes.AddRangeAsync(incomeTypeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteIncomeType(int incomeTypeId)
        {
            var income = await dataDbContext.IncomeTypes.FindAsync(incomeTypeId);
            if (income is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.IncomeTypes.Remove(income);
            await dataDbContext.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<IncomeType>> GetAllIncomeTypesAsync()
        {
            return await dataDbContext.IncomeTypes.ToListAsync();
        }

        public async Task<IncomeType?> GetIncomeTypeAsync(int incomeTypeId)
        {
            return await dataDbContext.IncomeTypes.FirstOrDefaultAsync(e => e.Id == incomeTypeId);
        }

        public async Task<IncomeType?> GetIncomeTypeByNameAsync(string incomeTypeName)
        {
            return await dataDbContext.IncomeTypes.FirstOrDefaultAsync(e => e.Name == incomeTypeName);
        }

        public async Task<IncomeType> UpdateIncomeType(int incomeTypeId, IncomeType incomeType)
        {
            IncomeType? incomeTypeToUpdate = await dataDbContext.IncomeTypes
                .FirstOrDefaultAsync(e => e.Id == incomeTypeId);

            if (incomeTypeToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            incomeTypeToUpdate.Name = incomeType.Name;
            await dataDbContext.SaveChangesAsync();
            return incomeTypeToUpdate;
        }
    }
}
