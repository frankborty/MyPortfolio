namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string PyName { get; set; } = string.Empty;
        public decimal Share { get; set; } = 1;
        public string Url { get; set; } = string.Empty;
        public decimal PMC { get; set; } = 1;
        public decimal CurrentValue { get; set; } = 1;
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public AssetCategoryDTO Category { get; set; } = new AssetCategoryDTO();

        public AssetDTO() { }
    }
}
