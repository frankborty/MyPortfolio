using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility.IncomeUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.IncomeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTypeController : ControllerBase
    {
        private readonly IIncomeTypeRepo _incomeTypeRepo;
        public IncomeTypeController(IIncomeTypeRepo incomeTypeRepo)
        {
            _incomeTypeRepo = incomeTypeRepo;
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Get all income type")]
        public async Task<IActionResult> GetAllIncomeTypesAsync()
        {
            try
            {
                var incomeTypeList = await _incomeTypeRepo.GetAllIncomeTypesAsync();
                if (incomeTypeList == null || !incomeTypeList.Any())
                {
                    return NotFound("Nessun tipo trovato");
                }
                List<IncomeTypeDTO> incomeListDto = new List<IncomeTypeDTO>();
                foreach (var incomeType in incomeTypeList)
                {
                    IncomeTypeDTO incomeTypeDto = IncomeTypeDTOConverter.ToIncomeTypeDTO(incomeType);
                    incomeListDto.Add(incomeTypeDto);
                }

                return Ok(incomeListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
        
        [HttpGet("{incomeTypeId}")]
        [SwaggerOperation(Summary = "Get income type by ID")]
        public async Task<IActionResult> GetIncomeTypeByIdAsync(int incomeTypeId)
        {
            try
            {
                var incomeType = await _incomeTypeRepo.GetIncomeTypeAsync(incomeTypeId);
                if (incomeType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                IncomeTypeDTO incomeTypeDto = IncomeTypeDTOConverter.ToIncomeTypeDTO(incomeType);
                return Ok(incomeTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
        
        [HttpGet]
        [Route("{incomeTypeName}/byName")]
        [SwaggerOperation(Summary = "Get income type by name")]
        public async Task<IActionResult> GetIncomeTypeByNameAsync(string incomeTypeName)
        {
            try
            {
                var incomeType = await _incomeTypeRepo.GetIncomeTypeByNameAsync(incomeTypeName);
                if (incomeType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                IncomeTypeDTO incomeTypeDto = IncomeTypeDTOConverter.ToIncomeTypeDTO(incomeType);
                return Ok(incomeTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive type")]
        public async Task<IActionResult> AddIncomeTypeAsync(string incomeType)
        {
            try
            {
                IncomeType incomeTypeToAdd = new IncomeType()
                {
                    Name = incomeType
                };
                await _incomeTypeRepo.AddIncomeTypeAsync(incomeTypeToAdd);
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
        public async Task<IActionResult> AddIncomeTypeListAsync([FromBody] List<string> incomeTypeList)
        {
            try
            {
                List<IncomeType> incomeTypeToAddList = new List<IncomeType>();
                foreach (var incomeType in incomeTypeList)
                {
                    IncomeType incomeTypeToAdd = new IncomeType()
                    {
                        Name = incomeType
                    };
                    incomeTypeToAddList.Add(incomeTypeToAdd);
                }
                if (incomeTypeToAddList.Count > 0)
                {
                    await _incomeTypeRepo.AddIncomeTypeListAsync(incomeTypeToAddList);
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
        [SwaggerOperation(Summary = "Delete income type")]
        public async Task<IActionResult> DeleteIncomeTypeAsync(int incomeTypeId)
        {
            try
            {
                await _incomeTypeRepo.DeleteIncomeTypeAsync(incomeTypeId);
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
        public async Task<IActionResult> DeleteIncomeTypeListAsync(List<int> incomeTypeIdList)
        {
            try
            {
                foreach (var incomeTypeId in incomeTypeIdList)
                {
                    await _incomeTypeRepo.DeleteIncomeTypeAsync(incomeTypeId);
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
        
        [HttpPut("{incomeTypeId}")]
        [SwaggerOperation(Summary = "Update income type")]
        public async Task<IActionResult> UpdateIncomeByIdAsync(int incomeTypeId, [FromBody] IncomeTypeDTO incomeTypeToUpdate)
        {
            try
            {
                IncomeType icomeTypeUpdated = new IncomeType()
                {
                    Name = incomeTypeToUpdate.Name
                };
                var incomeType = await _incomeTypeRepo.UpdateIncomeTypeAsync(incomeTypeId, icomeTypeUpdated);
                if (incomeType is null)
                {
                    return NotFound("Nessun tipo trovato");
                }

                IncomeTypeDTO incomeDto = IncomeTypeDTOConverter.ToIncomeTypeDTO(incomeType);
                return Ok(incomeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }
}
