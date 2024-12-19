using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Assets
{
    public class AssetType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public AssetCategory? Category { get; set; }
        public Collection<Asset>? Assets { get; set; }
    }
}
