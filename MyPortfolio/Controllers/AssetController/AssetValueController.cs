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
    public class AssetValueController : ControllerBase
    {
        private readonly IAssetValueRepo _assetValueRepo;
        public AssetValueController(IAssetValueRepo assetValueRepo)
        {
            _assetValueRepo = assetValueRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive value")]
        public async Task<IActionResult> GetAllAssetValues()
        {
            try
            {
                var assetValueList = await _assetValueRepo.GetAllAssetValueAsync();
                if (assetValueList == null || !assetValueList.Any())
                {
                    return NotFound("Nessun value trovato");
                }
                List<AssetValueDTO> assetListDto = new List<AssetValueDTO>();
                foreach (var assetValue in assetValueList)
                {
                    AssetValueDTO assetValueDto = AssetValueDTOConverter.ToAssetValueDTO(assetValue);
                    assetListDto.Add(assetValueDto);
                }

                return Ok(assetListDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{assetValueId}")]
        [SwaggerOperation(Summary = "Get asset value by ID")]
        public async Task<IActionResult> GetAssetValueById(int assetValueId)
        {
            try
            {
                var assetValue = await _assetValueRepo.GetAssetValueByIdAsync(assetValueId);
                if (assetValue is null)
                {
                    return NotFound("Nessuna value trovata.");
                }

                AssetValueDTO assetValueDto = AssetValueDTOConverter.ToAssetValueDTO(assetValue);
                return Ok(assetValueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{assetValueName}/byName")]
        [SwaggerOperation(Summary = "Get asset value by asse name")]
        public async Task<IActionResult> GetAssetValueListByName(string assetName)
        {
            try
            {
                var assetValueList = await _assetValueRepo.GetAssetValueByAssetNameAsync(assetName);
                if (assetValueList is null)
                {
                    return Ok(new List<AssetValue>());
                }

                List<AssetValueDTO> assetListDto = new List<AssetValueDTO>();
                foreach (var assetValue in assetValueList)
                {
                    AssetValueDTO assetValueDto = AssetValueDTOConverter.ToAssetValueDTO(assetValue);
                    assetListDto.Add(assetValueDto);
                }

                return Ok(assetListDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive value")]
        public async Task<IActionResult> AddAssetValue(AssetValueDTO assetValueDTO)
        {
            try
            {
                var assetValueToAdd = new AssetValue()
                {
                    AssetId = assetValueDTO.AssetId,
                    TimeStamp = assetValueDTO.TimeStamp,
                    Value = assetValueDTO.Value
                };
                await _assetValueRepo.AddAssetValueAsync(assetValueToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("addList")]
        [SwaggerOperation(Summary = "Add expensive value list")]
        public async Task<IActionResult> AddAssetValueList([FromBody] List<AssetValueDTO> assetValueList)
        {
            try
            {
                List<AssetValue> assetValueToAddList = new List<AssetValue>();
                foreach (var assetValueDTO in assetValueList)
                {
                    var assetValueToAdd = new AssetValue()
                    {
                        AssetId = assetValueDTO.AssetId,
                        TimeStamp = assetValueDTO.TimeStamp,
                        Value = assetValueDTO.Value
                    };
                    assetValueToAddList.Add(assetValueToAdd);
                }
                if (assetValueToAddList.Count > 0)
                {
                    await _assetValueRepo.AddAssetValueListAsync(assetValueToAddList);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete asset value")]
        public async Task<IActionResult> DeleteAssetValue(int assetValueId)
        {
            try
            {
                await _assetValueRepo.DeleteAssetValueAsync(assetValueId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("deleteList")]
        [SwaggerOperation(Summary = "Delete expensive value list")]
        public async Task<IActionResult> DeleteAssetValueList(List<int> assetValueIdList)
        {
            try
            {
                foreach (var assetValueId in assetValueIdList)
                {
                    await _assetValueRepo.DeleteAssetValueAsync(assetValueId);
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

        [HttpPut("{assetValueId}")]
        [SwaggerOperation(Summary = "Update asset value")]
        public async Task<IActionResult> UpdateAssetById(int assetValueId, AssetValueDTO newAssetValue)
        {
            try
            {
                var assetValueUpdated = new AssetValue()
                {
                    AssetId = newAssetValue.AssetId,
                    TimeStamp = newAssetValue.TimeStamp,
                    Value = newAssetValue.Value
                };
                var assetValue = await _assetValueRepo.UpdateAssetValueAsync(assetValueId, assetValueUpdated);
                if (assetValue is null)
                {
                    return NotFound("Nessuna value trovato");
                }

                AssetValueDTO assetDto = AssetValueDTOConverter.ToAssetValueDTO(assetValue);
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
