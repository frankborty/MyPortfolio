using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility;
using MyPortfolio.Utility.IncomeUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.IncomeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTypeController : ControllerBase
    {
        private readonly IIncomeTypeRepo _incomeTypeRepo;
        private readonly IIncomeRepo _incomeRepo;
        public IncomeTypeController(IIncomeTypeRepo incomeTypeRepo, IIncomeRepo incomeRepo)
        {
            _incomeTypeRepo = incomeTypeRepo;
            _incomeRepo = incomeRepo;
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
        [SwaggerOperation(Summary = "Add income type")]
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


        [HttpDelete]
        [SwaggerOperation(Summary = "Delete income type")]
        public async Task<IActionResult> DeleteIncomeTypeAsync(int incomeTypeId)
        {
            try
            {
                // se ho income con questo tipo non cancello
                var expenseList = await _incomeRepo.GetAllIncomesAsync();
                if (expenseList.Any(x => x.IncomeType?.Id == incomeTypeId))
                {
                    throw new Exception("Esistono entrate associate a questo tipo");
                }
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
