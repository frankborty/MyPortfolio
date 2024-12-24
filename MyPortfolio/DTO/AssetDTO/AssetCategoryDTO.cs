namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsInvested { get; set; } = false;
        public AssetCategoryDTO()
        {
            Id = -1;
        }
    }
}
