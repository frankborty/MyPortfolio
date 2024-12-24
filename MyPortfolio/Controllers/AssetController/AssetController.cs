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
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepo _assetRepo;
        public AssetController(IAssetRepo assetRepo)
        {
            _assetRepo = assetRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all assets")]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                var assetList = await _assetRepo.GetAllAssetAsync();
                if (assetList == null || !assetList.Any())
                {
                    return NotFound("Nessun asset trovato");
                }
                List<AssetDTO> assetListDto = new List<AssetDTO>();
                foreach (var asset in assetList)
                {
                    AssetDTO assetDto = AssetDTOConverter.ToAssetDTO(asset);
                    assetListDto.Add(assetDto);
                }

                return Ok(assetListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{assetId}")]
        [SwaggerOperation(Summary = "Get asset by ID")]
        public async Task<IActionResult> GetAssetById(int assetId)
        {
            try
            {
                var asset = await _assetRepo.GetAssetByIdAsync(assetId);
                if (asset is null)
                {
                    return NotFound("Nessun asset trovata.");
                }

                AssetDTO assetDto = AssetDTOConverter.ToAssetDTO(asset);
                return Ok(assetDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add asset")]
        public async Task<IActionResult> AddAsset(AssetToAddDTO asset)
        {
            try
            {
                Asset assetToAdd = AssetDTOConverter.FromAssetToAddDTO(asset);
                await AssetStaticUtils.AddSingleAsset(_assetRepo, assetToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("addList")]
        [SwaggerOperation(Summary = "Add asset list")]
        public async Task<IActionResult> AddAssetList([FromBody] List<AssetToAddDTO> assetList)
        {
            try
            {
                foreach (var asset in assetList)
                {
                    Asset assetToAdd = AssetDTOConverter.FromAssetToAddDTO(asset);
                    await AssetStaticUtils.AddSingleAsset(_assetRepo, assetToAdd);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete asset")]
        public async Task<IActionResult> DeleteAsset(int assetId)
        {
            try
            {
                await _assetRepo.DeleteAssetAsync(assetId);
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

        [HttpDelete]
        [Route("deleteList")]
        [SwaggerOperation(Summary = "Delete asset list")]
        public async Task<IActionResult> DeleteAssetList(List<int> assetIdList)
        {
            try
            {
                foreach (var assetId in assetIdList)
                {
                    await _assetRepo.DeleteAssetAsync(assetId);
                }
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

        [HttpPut("{assetId}")]
        [SwaggerOperation(Summary = "Update asset")]
        public async Task<IActionResult> UpdateAssetById(int assetId, [FromBody] AssetToAddDTO assetToUpdate)
        {
            try
            {
                Asset assetUpdated = AssetDTOConverter.FromAssetToAddDTO(assetToUpdate);
                var asset = await _assetRepo.UpdateAssetAsync(assetId, assetUpdated);
                if (asset is null)
                {
                    return NotFound("Nessun asset trovata.");
                }

                AssetDTO assetDto = AssetDTOConverter.ToAssetDTO(asset);
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
