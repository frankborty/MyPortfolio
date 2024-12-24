namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetToAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public decimal AvgPrice { get; set; }
        public int AssetTypeId { get; set; }

        public AssetToAddDTO() { }
    }
}
