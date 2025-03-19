using Microsoft.AspNetCore.Http;
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
    public class AssetOperationController : ControllerBase
    {
        private readonly IAssetRepo _assetRepo;
        private readonly IAssetValueRepo _assetValueRepo;
        private readonly IAssetOperationRepo _assetOperationRepo;
        public AssetOperationController(IAssetRepo assetRepo, IAssetValueRepo assetValueRepo, IAssetOperationRepo assetOperationRepo)
        {
            _assetRepo = assetRepo;
            _assetValueRepo = assetValueRepo;
            _assetOperationRepo = assetOperationRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all asset operation")]
        public async Task<IActionResult> GetAllAssetOperationsGroupedByAsset()
        {
            try
            {
                var assetOperationList = await _assetOperationRepo.GetAllAssetOperationAsync();
                if (assetOperationList == null || !assetOperationList.Any())
                {
                    return NotFound("Nessuna operazione trovata");
                }
                List<AssetOperationDTO> operationListDTO = new List<AssetOperationDTO>();
                foreach (var operation in assetOperationList)
                {
                    operationListDTO.Add(AssetOperationDTOConverter.ToAssetOperationDTO(operation));
                }
                return Ok(operationListDTO);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("groupedByAsset")]
        [SwaggerOperation(Summary = "Get all asset operation grouped by asset")]
        public async Task<IActionResult> GetAllAssetOperations()
        {
            try
            {
                var assetOperationList = await _assetOperationRepo.GetAllAssetOperationWithAssetAsync();
                if (assetOperationList == null || !assetOperationList.Any())
                {
                    return NotFound("Nessuna operazione trovata");
                }
                var groupedAssetOperationList = assetOperationList.GroupBy(o => o.Asset).ToList();
                AssetWithOperationListDTO assetWithOperationList = new AssetWithOperationListDTO();

                List<AssetWithOperationListDTO> resultDto = new List<AssetWithOperationListDTO>();
                foreach (var assetOperation in groupedAssetOperationList)
                {
                    AssetWithOperationListDTO assetOperationListDTO = new AssetWithOperationListDTO()
                    {
                        Asset = AssetDTOConverter.ToAssetDTO(assetOperation.Key!)
                    };
                    foreach (var operation in assetOperation)
                    {
                        assetOperationListDTO.OperationList.Add(AssetOperationDTOConverter.ToAssetOperationDTO(operation));
                    }
                    resultDto.Add(assetOperationListDTO);

                }
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add asset operation")]
        public async Task<IActionResult> AddAssetOperation(AssetOperationDTO assetOperation)
        {
            try
            {
                AssetOperation assetOperationToAdd = AssetOperationDTOConverter.FromAssetOperationDTO(assetOperation);
                AssetOperation? newAssetOperation = await _assetOperationRepo.AddAssetOperationAsync(assetOperationToAdd);
                if (newAssetOperation is null)
                {
                    throw new Exception("Creato asset operation null");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }


        [HttpDelete("{assetOperationId}")]
        [SwaggerOperation(Summary = "Delete asset operation")]
        public async Task<IActionResult> DeleteAssetOperation(int assetOperationId)
        {
            try
            {
                await _assetOperationRepo.DeleteAssetOperationAsync(assetOperationId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPut("{assetOperationId}")]
        [SwaggerOperation(Summary = "Update asset operation")]
        public async Task<IActionResult> UpdateAssetOperationById(int assetOperationId, [FromBody] AssetOperationDTO newAssetOperation)
        {
            try
            {
                AssetOperation assetOperationUpdated = AssetOperationDTOConverter.FromAssetOperationDTO(newAssetOperation);
                var asset = await _assetOperationRepo.UpdateAssetOperationAsync(assetOperationId, assetOperationUpdated);
                if (asset is null)
                {
                    return NotFound("Nessun asset operarion trovata.");
                }

                AssetOperationDTO assetOperationDto = AssetOperationDTOConverter.ToAssetOperationDTO(assetOperationUpdated);
                return Ok(assetOperationDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }
}
