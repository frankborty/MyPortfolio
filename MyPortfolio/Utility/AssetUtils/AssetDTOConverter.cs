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
                Url = asset.Url,
                Note = asset.Note,
                Share = asset.Share,
                Category = AssetCategoryDTOConverter.ToAssetCategoryDTO(asset.Category)
            };
        }
        public static AssetSimpleDTO ToAssetSimpleDTO(Asset asset)
        {
            return new AssetSimpleDTO()
            {
                Id = asset.Id,
                Name = asset.Name,
                Category = AssetCategoryDTOConverter.ToAssetCategoryDTO(asset.Category)
            };
        }

        public static Asset FromAssetDTO(AssetDTO assetToAdd)
        {
            return new Asset()
            {
                ISIN = assetToAdd.ISIN,
                Name = assetToAdd.Name,
                Note = assetToAdd.Note,
                Share = assetToAdd.Share,
                Url = assetToAdd.Url,
                CategoryId = assetToAdd.Category.Id
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

    public static class AssetValueDTOConverter
    {
        public static AssetValueDTO ToAssetValueDTO(AssetValue? assetValue)
        {
            if (assetValue is null)
            {
                return new AssetValueDTO();
            }
            return new AssetValueDTO()
            {
                Id = assetValue.Id,
                AssetId = assetValue.AssetId,
                Value = assetValue.Value,
                TimeStamp = GenericUtils.ConvertDateTimeToString(assetValue.TimeStamp)
            };
        }
    }
}

