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
                TimeStamp = assetValue.TimeStamp
            };
        }
    }

    public static class AssetOperationDTOConverter
    {
        public static AssetOperationDTO ToAssetOperationDTO(AssetOperation assetOperation)
        {
            AssetOperationDTO result = new AssetOperationDTO();
            if (assetOperation is null)
            {
                return result;
            }
            result.AssetId = assetOperation.AssetId;
            result.OperationType = assetOperation.OperationType;
            result.Date = assetOperation.Date;
            result.Share = assetOperation.Share;
            result.PMC = assetOperation.PMC;
            return result;
        }

        public static AssetOperation FromAssetOperationDTO(AssetOperationDTO assetOperationDTO)
        {
            return new AssetOperation()
            {
                AssetId = assetOperationDTO.AssetId,
                Date = assetOperationDTO.Date,
                OperationType = assetOperationDTO.OperationType,
                PMC = assetOperationDTO.PMC,
                Share = assetOperationDTO.Share
            };
        }
    }
}

