namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public decimal AvgPrice { get; set; }
        public DateTime TimeStamp { get; set; }
        public AssetCategoryDTO AssetCategory { get; set; } = new AssetCategoryDTO();

        public AssetDTO() { }
    }
}
