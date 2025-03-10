using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.DTO.AssetDTO;
using MyPortfolio.Models.Assets;
using MyPortfolio.Utility;
using MyPortfolio.Utility.AssetUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.AssetController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetValueController : ControllerBase
    {
        private readonly IAssetValueRepo _assetValueRepo;
        private readonly IAssetRepo _assetRepo;
        public AssetValueController(IAssetValueRepo assetValueRepo, IAssetRepo assetRepo)
        {
            _assetValueRepo = assetValueRepo;
            _assetRepo = assetRepo;
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
        [Route("financialVariation")]
        [SwaggerOperation(Summary = "Get asset variation list")]
        //ToDo: devo modificarla perchè la variazione si calcola sul prezzo medio di acquisto e sul prezzo attuale
        public async Task<IActionResult> GetFinancialAssetVariation()
        {
            try
            {
                List<FinancialAssetValuationDTO> assetVariationList = new List<FinancialAssetValuationDTO>();
                var assetList = await _assetRepo.GetAllAssetAsync();
                IEnumerable<IGrouping<Asset, AssetValue>> allAssetValueListQuery = await _assetValueRepo.GetAllAssetValueGroupByAssetIdAsync();
                var allAssetVaueList = allAssetValueListQuery.ToList();

                foreach (var asset in assetList)
                {
                    if (asset.Category?.IsInvested != true)
                    {
                        continue;
                    }
                    FinancialAssetValuationDTO assetVariation = new FinancialAssetValuationDTO()
                    {
                        Asset = AssetDTOConverter.ToAssetDTO(asset)
                    };
                    foreach(var assetValue in allAssetVaueList)
                    {
                        if (assetValue.Key == asset)
                        {
                            decimal firstPrice = assetValue.First().Value;
                            decimal lastPrice = assetValue.Last().Value;
                            assetVariation.InitialValue = firstPrice;
                            assetVariation.FinalValue = lastPrice;
                            assetVariation.AbsDelta = lastPrice - firstPrice;
                            assetVariation.PercentDelta = ((lastPrice - firstPrice) / firstPrice) * 100;
                            break;
                        }
                    }
                    assetVariationList.Add(assetVariation);
                }

                return Ok(assetVariationList);
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
                var asset = await _assetRepo.GetAssetByIdAsync(assetValueDTO.AssetId);
                if(asset is null)
                {
                    return NotFound($"Asset {assetValueDTO.AssetId} not found");
                }
                var assetValueToAdd = new AssetValue()
                {
                    AssetId = assetValueDTO.AssetId,
                    TimeStamp = assetValueDTO.TimeStamp,
                    Value = assetValueDTO.Value,
                    Asset = asset
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

        [HttpGet]
        [Route("SummaryByMonth")]
        [SwaggerOperation(Summary = "Get asset month vakye")]
        public async Task<IActionResult> GetAssetValueListByMonth()
        {
            try
            {
                IEnumerable<IGrouping<Asset, AssetValue>> allAssetValueListQuery = await _assetValueRepo.GetAllAssetValueWithDetailsGroupByAssetIdAsync();
                var allAssetVaueList = allAssetValueListQuery.ToList();


                List<AssetValueListDTO> result = new List<AssetValueListDTO>();
                foreach (var assetValueList in allAssetVaueList) {
                    result.Add(AssetStaticUtils.CreateMonthValueList(assetValueList));
                }

                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("SummaryByMonth")]
        [SwaggerOperation(Summary = "Get asset month vakye")]
        public async Task<IActionResult> SetAssetValueListByMonth([FromBody] AssetValueListDTO assetValueByMonth)
        {
            try
            {
                IEnumerable<AssetValue> allAssetValueList = await _assetValueRepo.GetAssetValueByAssetIdAsync(assetValueByMonth.Asset.Id);
                if (allAssetValueList.Count() == 0)
                {
                    return Ok();
                }

                IEnumerable<IGrouping<string, AssetValue>> storedAssetVaueList = allAssetValueList.OrderBy(a => a.TimeStamp).GroupBy(a => a.TimeStamp.ToString("yyyyMM"));

                foreach (AssetValueDTO assetNewValue in assetValueByMonth.AssetValueList)
                {
                    var monthYearStoredVallue = storedAssetVaueList.FirstOrDefault(g => g.Key == assetNewValue.TimeStamp.ToString("yyyyMM"))?.ToList();
                    if(monthYearStoredVallue is null)
                    {
                        var assetToInsert = await _assetRepo.GetAssetByIdAsync(assetValueByMonth.Asset.Id);
                        if (assetToInsert is null)
                        {
                            return NotFound($"Asset {assetValueByMonth.Asset.Id} not found");
                        }
                        //aggiungo il nuovo valore con iil primo giorno del mese
                        var assetValueToAdd = new AssetValue()
                        {
                            AssetId = assetValueByMonth.Asset.Id,
                            TimeStamp = assetNewValue.TimeStamp,
                            Value = assetNewValue.Value,
                            Asset = assetToInsert ?? new Asset()
                        };
                        await _assetValueRepo.AddAssetValueAsync(assetValueToAdd);
                    }
                    else
                    {
                        //aggiorno il valore dell'asset con data maggiore
                        var assetToUpdate = monthYearStoredVallue.OrderByDescending(a => a.TimeStamp).First();
                        assetToUpdate.Value = assetNewValue.Value;
                        await _assetValueRepo.UpdateAssetValueAsync(assetToUpdate.Id, assetToUpdate);
                    }

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

    }
}
