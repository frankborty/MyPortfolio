using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Assets
{
    public class AssetCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsInvested { get; set; } = false;
        public Collection<AssetType>? AssetTypes { get; set; }
    }
}
