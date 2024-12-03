using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Incomes;

namespace MyPortfolio.Data.Repositories.IncomeRepo
{
    public class IncomeRepo : IIncomeRepo
    {
        private readonly DataDbContext dataDbContext;

        public IncomeRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<Income?> GetIncomeAsync(int incomeId)
        {
            return await dataDbContext.Incomes
                .Include(e => e.IncomeType)
                .FirstOrDefaultAsync(e => e.Id == incomeId);
        }

        public async Task<IEnumerable<Income>> GetAllIncomesAsync()
        {
            return await dataDbContext.Incomes
                .Include(e => e.IncomeType)
                .ToListAsync();
        }

        public async Task<Income> AddIncomeAsync(Income income)
        {
            var result = await dataDbContext.Incomes.AddAsync(income);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteIncomeAsync(int incomeId)
        {
            var income = await dataDbContext.Incomes.FindAsync(incomeId);
            if (income != null)
            {
                dataDbContext.Incomes.Remove(income);
                await dataDbContext.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }

        public async Task<Income> UpdateIncomeAsync(int incomeId, Income income)
        {
            Income? incomeToUpdate = await dataDbContext.Incomes
                .FirstOrDefaultAsync(e => e.Id == incomeId);

            if (incomeToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            incomeToUpdate.Amount = income.Amount;
            incomeToUpdate.Date = income.Date;
            incomeToUpdate.TypeId = income.TypeId;
            await dataDbContext.SaveChangesAsync();
            return incomeToUpdate;
        }

        public async Task AddIncomeListAsync(List<Income> incomeList)
        {
            await dataDbContext.Incomes.AddRangeAsync(incomeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }
    }
}
