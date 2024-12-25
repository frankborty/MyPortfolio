using MyPortfolio.DTO.AssetDTO;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Utility.AssetUtils
{
    public static class AssetDTOConverter
    {
        public static AssetDTO ToAssetDTO(Asset asset)
        {
            return new AssetDTO()
            {
                Id = asset.Id,
                Name = asset.Name,
                ISIN = asset.ISIN,
                Balance = asset.Balance,
                Note = asset.Note,
                Share = asset.Share,
                AvgPrice = asset.AvgPrice,
                TimeStamp = asset.TimeStamp,
                AssetCategory = AssetCategoryDTOConverter.ToAssetCategoryDTO(asset.AssetCategory)
            };
        }

        public static Asset FromAssetToAddDTO(AssetToAddDTO assetToAdd)
        {
            return new Asset()
            {
                AvgPrice = assetToAdd.AvgPrice,
                Balance = assetToAdd.Balance,
                ISIN = assetToAdd.ISIN,
                Name = assetToAdd.Name,
                Note = assetToAdd.Note,
                Share = assetToAdd.Share,
                TimeStamp = assetToAdd.TimeStamp,
                CategoryId = assetToAdd.AssetCategoryId
            };
        }
    }

    public static class AssetCategoryDTOConverter
    {
        public static AssetCategoryDTO ToAssetCategoryDTO(AssetCategory? assetCategory)
        {
            if (assetCategory is null)
            {
                return new AssetCategoryDTO();
            }
            return new AssetCategoryDTO()
            {
                Id = assetCategory.Id,
                Name = assetCategory.Name,
                IsInvested = assetCategory.IsInvested,
            };
        }
    }
}

