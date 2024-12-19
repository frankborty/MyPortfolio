using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public class AssetTypeRepo : IAssetTypeRepo
    {
        private readonly DataDbContext dataDbContext;

        public AssetTypeRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<AssetType?> AddAssetTypeAsync(AssetType assetType)
        {
            var result = await dataDbContext.AssetTypes.AddAsync(assetType);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task AddAssetTypeListAsync(List<AssetType> assetTypeList)
        {
            await dataDbContext.AssetTypes.AddRangeAsync(assetTypeList);
            await dataDbContext.SaveChangesAsync();
            return;
        }

        public async Task DeleteAssetTypeAsync(int assetTypeId)
        {
            var assetType = await dataDbContext.AssetTypes.FindAsync(assetTypeId);
            if (assetType is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.AssetTypes.Remove(assetType);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetType>> GetAllAssetTypeAsync()
        {
            return await dataDbContext.AssetTypes.ToListAsync();
        }

        public async Task<AssetType?> GetAssetTypeByIdAsync(int assetTypeId)
        {
            return await dataDbContext.AssetTypes.FirstOrDefaultAsync(e => e.Id == assetTypeId);
        }

        public async Task<AssetType> UpdateAssetTypeAsync(int assetTypeId, AssetType assetType)
        {
            AssetType? assetTypeToUpdate = await dataDbContext.AssetTypes
                .FirstOrDefaultAsync(e => e.Id == assetTypeId);

            if (assetTypeToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            assetTypeToUpdate.Name = assetTypeToUpdate.Name;
            assetTypeToUpdate.CategoryId = assetTypeToUpdate.CategoryId;
            await dataDbContext.SaveChangesAsync();
            return assetTypeToUpdate;
        }
    }
}
