using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.DTO.AssetDTO;
using MyPortfolio.Models.Assets;
using MyPortfolio.Utility.AssetUtils;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace MyPortfolio.Controllers.AssetController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepo _assetRepo;
        private readonly IAssetValueRepo _assetValueRepo;
        private readonly IAssetOperationRepo _assetOperationRepo;
        public AssetController(IAssetRepo assetRepo, IAssetValueRepo assetValueRepo, IAssetOperationRepo assetOperationRepo)
        {
            _assetRepo = assetRepo;
            _assetValueRepo = assetValueRepo;
            _assetOperationRepo = assetOperationRepo;
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

        [HttpGet]
        [Route("withValue")]
        [SwaggerOperation(Summary = "Get all assets with current value")]
        public async Task<IActionResult> GetAllAssetsWithValue()
        {
            try
            {
                List<AssetValue> assetValueList = (await _assetValueRepo.GetAllAssetValueAsync()).OrderByDescending(x=>x.TimeStamp).ToList();
                Dictionary<int, List<AssetOperation>> assetOperationGroupedByAssetId = (await _assetOperationRepo.GetAllAssetOperationAsync()).GroupBy(o=>o.AssetId).ToDictionary(g => g.Key, g => g.ToList());
                var assetList = await _assetRepo.GetAllAssetAsync();
                if (assetList == null || !assetList.Any())
                {
                    return NotFound("Nessun asset trovato");
                }
                List<AssetDTO> assetListDto = new List<AssetDTO>();
                foreach (var asset in assetList)
                {
                    AssetDTO assetDto = AssetDTOConverter.ToAssetDTO(asset);
                    assetDto.CurrentValue = assetValueList.FirstOrDefault(x => x.AssetId == assetDto.Id)?.Value ?? -1;

                    decimal spesaTotale = 0;

                    //calcolo il numero di share
                    if (assetOperationGroupedByAssetId.TryGetValue(asset.Id, out var values)) {
                        assetDto.Share = values.Sum(x => x.OperationType == "BUY" ? x.Share : -x.Share);
                        spesaTotale = values.Sum(x => x.OperationType == "BUY" ? x.PMC * x.Share : 0);
                    }
                    if (assetDto.Share != 0)
                    {
                        assetDto.PMC = spesaTotale / assetDto.Share;
                    }
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
        public async Task<IActionResult> AddAsset(AssetDTO asset)
        {
            try
            {
                Asset assetToAdd = AssetDTOConverter.FromAssetDTO(asset);
                Asset? newAsset = await _assetRepo.AddAssetAsync(assetToAdd);
                if (newAsset is null)
                {
                    throw new Exception("Creato asset null");
                }

                //aggiunto anche un assetValue con data odierna e balance nullo
                Asset? createdAsset = await _assetRepo.GetAssetByIdAsync(newAsset.Id);
                if (createdAsset is null)
                {
                    throw new Exception("Creato asset null");
                }
                var assetValueToAdd = new AssetValue()
                {
                    AssetId = newAsset.Id,
                    TimeStamp = DateTime.Now,
                    Value = 0,
                    Asset = createdAsset
                };
                await _assetValueRepo.AddAssetValueAsync(assetValueToAdd);
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


        [HttpPut("{assetId}")]
        [SwaggerOperation(Summary = "Update asset")]
        public async Task<IActionResult> UpdateAssetById(int assetId, [FromBody] AssetDTO newAsset)
        {
            try
            {
                Asset assetUpdated = AssetDTOConverter.FromAssetDTO(newAsset);
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
