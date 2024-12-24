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
    public class AssetTypeController : ControllerBase
    {
        private readonly IAssetTypeRepo _assetTypeRepo;
        public AssetTypeController(IAssetTypeRepo assetTypeRepo)
        {
            _assetTypeRepo = assetTypeRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive type")]
        public async Task<IActionResult> GetAllAssetTypes()
        {
            try
            {
                var assetTypeList = await _assetTypeRepo.GetAllAssetTypeAsync();
                if (assetTypeList == null || !assetTypeList.Any())
                {
                    return NotFound("Nessun tipo trovato");
                }
                List<AssetTypeDTO> assetListDto = new List<AssetTypeDTO>();
                foreach (var assetType in assetTypeList)
                {
                    AssetTypeDTO assetTypeDto = AssetTypeDTOConverter.ToAssetTypeDTO(assetType);
                    assetListDto.Add(assetTypeDto);
                }

                return Ok(assetListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{assetTypeId}")]
        [SwaggerOperation(Summary = "Get asset type by ID")]
        public async Task<IActionResult> GetAssetTypeById(int assetTypeId)
        {
            try
            {
                var assetType = await _assetTypeRepo.GetAssetTypeByIdAsync(assetTypeId);
                if (assetType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                AssetTypeDTO assetTypeDto = AssetTypeDTOConverter.ToAssetTypeDTO(assetType);
                return Ok(assetTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{assetTypeName}/byName")]
        [SwaggerOperation(Summary = "Get asset type by name")]
        public async Task<IActionResult> GetAssetTypeByName(string assetTypeName)
        {
            try
            {
                var assetType = await _assetTypeRepo.GetAssetTypeByNameAsync(assetTypeName);
                if (assetType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                AssetTypeDTO assetTypeDto = AssetTypeDTOConverter.ToAssetTypeDTO(assetType);
                return Ok(assetTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive type")]
        public async Task<IActionResult> AddAssetType(AssetTypeToAddDTO assetType)
        {
            try
            {
                AssetType assetTypeToAdd = AssetTypeDTOConverter.FromAssetTypeToAddDTO(assetType);
                await _assetTypeRepo.AddAssetTypeAsync(assetTypeToAdd);
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
        [SwaggerOperation(Summary = "Add expensive type list")]
        public async Task<IActionResult> AddAssetTypeList([FromBody] List<AssetTypeToAddDTO> assetTypeList)
        {
            try
            {
                List<AssetType> assetTypeToAddList = new List<AssetType>();
                foreach (var assetType in assetTypeList)
                {
                    AssetType assetTypeToAdd = AssetTypeDTOConverter.FromAssetTypeToAddDTO(assetType);
                    assetTypeToAddList.Add(assetTypeToAdd);
                }
                if (assetTypeToAddList.Count > 0)
                {
                    await _assetTypeRepo.AddAssetTypeListAsync(assetTypeToAddList);
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
        [SwaggerOperation(Summary = "Delete asset type")]
        public async Task<IActionResult> DeleteAssetType(int assetTypeId)
        {
            try
            {
                await _assetTypeRepo.DeleteAssetTypeAsync(assetTypeId);
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
        [SwaggerOperation(Summary = "Delete expensive type list")]
        public async Task<IActionResult> DeleteAssetTypeList(List<int> assetTypeIdList)
        {
            try
            {
                foreach (var assetTypeId in assetTypeIdList)
                {
                    await _assetTypeRepo.DeleteAssetTypeAsync(assetTypeId);
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

        [HttpPut("{assetTypeId}")]
        [SwaggerOperation(Summary = "Update asset type")]
        public async Task<IActionResult> UpdateAssetById(int assetTypeId, [FromBody] AssetTypeToAddDTO assetTypeToUpdate)
        {
            try
            {
                AssetType assetTypeUpdated = AssetTypeDTOConverter.FromAssetTypeToAddDTO(assetTypeToUpdate);
                var assetType = await _assetTypeRepo.UpdateAssetTypeAsync(assetTypeId, assetTypeUpdated);
                if (assetType is null)
                {
                    return NotFound("Nessun tipo trovato");
                }

                AssetTypeDTO assetDto = AssetTypeDTOConverter.ToAssetTypeDTO(assetType);
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
