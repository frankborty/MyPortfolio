using MyPortfolio.Models.Assets;

namespace MyPortfolio.Utility.AssetUtils
{
    public class AssetStaticUtils
    {
        internal static AssetCategory? GetCategoryFromName(string assetCategoryName, List<AssetCategory> assetCategoryList)
        {
            foreach (AssetCategory assetCategory in assetCategoryList)
            {
                if (string.Equals(assetCategoryName, assetCategory.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assetCategory;
                }
            }
            return null;
        }


        internal static AssetType? GetTypeFromName(string assetTypeName, List<AssetType> assetTypeList)
        {
            foreach (AssetType assetType in assetTypeList)
            {
                if (string.Equals(assetTypeName, assetType.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assetType;
                }
            }
            return null;
        }
    }
}
