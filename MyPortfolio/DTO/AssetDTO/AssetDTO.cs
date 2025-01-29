namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public decimal AvgPrice { get; set; }
        public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public AssetCategoryDTO AssetCategory { get; set; } = new AssetCategoryDTO();

        public AssetDTO() { }
    }
}
