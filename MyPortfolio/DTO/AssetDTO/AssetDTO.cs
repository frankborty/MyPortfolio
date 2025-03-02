namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public string Url { get; set; } = string.Empty;
        public int PMC { get; set; } = 0;
        public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public AssetCategoryDTO Category { get; set; } = new AssetCategoryDTO();

        public AssetDTO() { }
    }
}
