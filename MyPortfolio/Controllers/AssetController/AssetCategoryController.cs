using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.DTO.AssetDTO;
using MyPortfolio.Models.Assets;
using MyPortfolio.Utility.AssetUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.AssetController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetCategoryController : ControllerBase
    {
        private readonly IAssetCategoryRepo _assetCategoryRepo;
        private readonly IAssetRepo _assetRepo;
        public AssetCategoryController(IAssetCategoryRepo assetCategoryRepo, IAssetRepo assetRepo)
        {
            _assetCategoryRepo = assetCategoryRepo;
            _assetRepo = assetRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive category")]
        public async Task<IActionResult> GetAllAssetCategorys()
        {
            try
            {
                var assetCategoryList = await _assetCategoryRepo.GetAllAssetCategoryAsync();
                if (assetCategoryList == null || !assetCategoryList.Any())
                {
                    return NotFound("Nessuna categroy trovata");
                }
                List<AssetCategoryDTO> assetListDto = new List<AssetCategoryDTO>();
                foreach (var assetCategory in assetCategoryList)
                {
                    AssetCategoryDTO assetCategoryDto = AssetCategoryDTOConverter.ToAssetCategoryDTO(assetCategory);
                    assetListDto.Add(assetCategoryDto);
                }

                return Ok(assetListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{assetCategoryId}")]
        [SwaggerOperation(Summary = "Get asset category by ID")]
        public async Task<IActionResult> GetAssetCategoryById(int assetCategoryId)
        {
            try
            {
                AssetCategory? assetCategory = await _assetCategoryRepo.GetAssetCategoryByIdAsync(assetCategoryId);
                if (assetCategory is null)
                {
                    return NotFound("Nessuna category trovata.");
                }

                AssetCategoryDTO assetCategoryDto = AssetCategoryDTOConverter.ToAssetCategoryDTO(assetCategory);
                return Ok(assetCategoryDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{assetCategoryName}/byName")]
        [SwaggerOperation(Summary = "Get asset category by name")]
        public async Task<IActionResult> GetAssetByName(string assetCategoryName)
        {
            try
            {
                var assetCategory = await _assetCategoryRepo.GetAssetCategoryByNameAsync(assetCategoryName);
                if (assetCategory is null)
                {
                    return NotFound("Nessuna category trovata.");
                }

                AssetCategoryDTO assetCategoryDto = AssetCategoryDTOConverter.ToAssetCategoryDTO(assetCategory);
                return Ok(assetCategoryDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive category")]
        public async Task<IActionResult> AddAssetCategory(AssetCategoryDTO assetCategory)
        {
            try
            {
                AssetCategory assetCategoryToAdd = new AssetCategory()
                {
                    Name = assetCategory.Name,
                    IsInvested = assetCategory.IsInvested,
                };
                await _assetCategoryRepo.AddAssetCategoryAsync(assetCategoryToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete asset category")]
        public async Task<IActionResult> DeleteAssetCategory(int assetCategoryId)
        {
            try
            {
                // se ho asset con questa categoria non cancello
                var assetList = await _assetRepo.GetAllAssetAsync();
                if (assetList.Any(x => x.Category?.Id == assetCategoryId))
                {
                    return Conflict("Esistono asset associate a questo tipo");
                }
                await _assetCategoryRepo.DeleteAssetCategoryAsync(assetCategoryId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPut("{assetCategoryId}")]
        [SwaggerOperation(Summary = "Update asset category")]
        public async Task<IActionResult> UpdateAssetCategoryById(int assetCategoryId, [FromBody] AssetCategoryDTO newAssetCategory)
        {
            try
            {
                AssetCategory assetCategoryToAdd = new AssetCategory()
                {
                    Name = newAssetCategory.Name,
                    IsInvested = newAssetCategory.IsInvested,
                };
                var assetCategory = await _assetCategoryRepo.UpdateAssetCategoryAsync(assetCategoryId, assetCategoryToAdd);
                if (assetCategory is null)
                {
                    return NotFound("Nessuna category trovato");
                }

                AssetCategoryDTO assetDto = AssetCategoryDTOConverter.ToAssetCategoryDTO(assetCategory);
                return Ok(assetDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }
}
