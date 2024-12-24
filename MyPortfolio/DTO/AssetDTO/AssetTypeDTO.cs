using MyPortfolio.DTO.ExpenseDTO;

namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AssetCategoryDTO Category { get; set; } = new AssetCategoryDTO();
        public AssetTypeDTO() { }
    }
}
