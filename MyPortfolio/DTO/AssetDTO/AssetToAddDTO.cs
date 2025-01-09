namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetToAddDTO
    {
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public int AssetCategoryId { get; set; }

        public AssetToAddDTO() { }
    }
}
