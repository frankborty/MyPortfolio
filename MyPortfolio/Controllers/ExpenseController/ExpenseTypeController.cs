using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Utility.ExpenseUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.ExpenseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        public ExpenseTypeController(IExpenseTypeRepo expenseTypeRepo)
        {
            _expenseTypeRepo = expenseTypeRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive type")]
        public async Task<IActionResult> GetAllExpenseTypes()
        {
            try
            {
                var expenseTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                if (expenseTypeList == null || !expenseTypeList.Any())
                {
                    return NotFound("Nessun tipo trovato");
                }
                List<ExpenseTypeDTO> expenseListDto = new List<ExpenseTypeDTO>();
                foreach (var expenseType in expenseTypeList)
                {
                    ExpenseTypeDTO expenseTypeDto = ExpenseTypeDTOConverter.ToExpenseTypeDTO(expenseType);
                    expenseListDto.Add(expenseTypeDto);
                }

                return Ok(expenseListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{expenseTypeId}")]
        [SwaggerOperation(Summary = "Get expense type by ID")]
        public async Task<IActionResult> GetExpenseTypeById(int expenseTypeId)
        {
            try
            {
                var expenseType = await _expenseTypeRepo.GetExpenseTypeAsync(expenseTypeId);
                if (expenseType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                ExpenseTypeDTO expenseTypeDto = ExpenseTypeDTOConverter.ToExpenseTypeDTO(expenseType);
                return Ok(expenseTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{expenseTypeName}/byName")]
        [SwaggerOperation(Summary = "Get expense type by name")]
        public async Task<IActionResult> GetExpenseTypeByName(string expenseTypeName)
        {
            try
            {
                var expenseType = await _expenseTypeRepo.GetExpenseTypeByNameAsync(expenseTypeName);
                if (expenseType is null)
                {
                    return NotFound("Nessun tipo trovata.");
                }

                ExpenseTypeDTO expenseTypeDto = ExpenseTypeDTOConverter.ToExpenseTypeDTO(expenseType);
                return Ok(expenseTypeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive type")]
        public async Task<IActionResult> AddExpenseType(ExpenseTypeToAddDTO expenseType)
        {
            try
            {
                ExpenseType expenseTypeToAdd = ExpenseTypeDTOConverter.FromExpenseTypeToAddDTO(expenseType);
                await _expenseTypeRepo.AddExpenseTypeAsync(expenseTypeToAdd);
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
        public async Task<IActionResult> AddExpenseTypeList([FromBody] List<ExpenseTypeToAddDTO> expenseTypeList)
        {
            try
            {
                List<ExpenseType> expenseTypeToAddList = new List<ExpenseType>();
                foreach (var expenseType in expenseTypeList)
                {
                    ExpenseType expenseTypeToAdd = ExpenseTypeDTOConverter.FromExpenseTypeToAddDTO(expenseType);
                    expenseTypeToAddList.Add(expenseTypeToAdd);
                }
                if (expenseTypeToAddList.Count > 0)
                {
                    await _expenseTypeRepo.AddExpenseTypeListAsync(expenseTypeToAddList);
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
        [SwaggerOperation(Summary = "Delete expense type")]
        public async Task<IActionResult> DeleteExpenseType(int expenseTypeId)
        {
            try
            {
                await _expenseTypeRepo.DeleteExpenseTypeAsync(expenseTypeId);
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
        public async Task<IActionResult> DeleteExpenseTypeList(List<int> expenseTypeIdList)
        {
            try
            {
                foreach (var expenseTypeId in expenseTypeIdList)
                {
                    await _expenseTypeRepo.DeleteExpenseTypeAsync(expenseTypeId);
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

        [HttpPut("{expenseTypeId}")]
        [SwaggerOperation(Summary = "Update expense type")]
        public async Task<IActionResult> UpdateExpenseById(int expenseTypeId, [FromBody] ExpenseTypeToAddDTO expenseTypeToUpdate)
        {
            try
            {
                ExpenseType expenseTypeUpdated = ExpenseTypeDTOConverter.FromExpenseTypeToAddDTO(expenseTypeToUpdate);
                var expenseType = await _expenseTypeRepo.UpdateExpenseTypeAsync(expenseTypeId, expenseTypeUpdated);
                if (expenseType is null)
                {
                    return NotFound("Nessun tipo trovato");
                }

                ExpenseTypeDTO expenseDto = ExpenseTypeDTOConverter.ToExpenseTypeDTO(expenseType);
                return Ok(expenseDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }
}
