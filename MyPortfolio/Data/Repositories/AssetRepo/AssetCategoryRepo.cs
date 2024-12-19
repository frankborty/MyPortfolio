using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public class AssetCategoryRepo : IAssetCategoryRepo
    {
        private readonly DataDbContext dataDbContext;

        public AssetCategoryRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<AssetCategory?> AddAssetCategoryAsync(AssetCategory assetCategory)
        {
            var result = await dataDbContext.AssetCategories.AddAsync(assetCategory);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddAssetCategoryListAsync(List<AssetCategory> assetCategoryList)
        {
            await dataDbContext.AssetCategories.AddRangeAsync(assetCategoryList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteAssetCategoryAsync(int assetCategoryId)
        {
            var assetCategory = await dataDbContext.AssetCategories.FindAsync(assetCategoryId);
            if (assetCategory is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.AssetCategories.Remove(assetCategory);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetCategory>> GetAllAssetCategoryAsync()
        {
            return await dataDbContext.AssetCategories.ToListAsync();
        }

        public async Task<AssetCategory?> GetAssetCategoryByIdAsync(int assetCategoryId)
        {
            return await dataDbContext.AssetCategories
                .Include(e => e.AssetTypes)
                .FirstOrDefaultAsync(e => e.Id == assetCategoryId);
        }

        public async Task<AssetCategory> UpdateAssetCategoryAsync(int assetCategoryId, AssetCategory assetCategory)
        {
            AssetCategory? assetCategoryToUpdate = await dataDbContext.AssetCategories
                .FirstOrDefaultAsync(e => e.Id == assetCategoryId);

            if (assetCategoryToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            assetCategoryToUpdate.Name = assetCategoryToUpdate.Name;
            assetCategoryToUpdate.IsInvested = assetCategoryToUpdate.IsInvested;
            await dataDbContext.SaveChangesAsync();
            return assetCategoryToUpdate;
        }
    }
}
