using Microsoft.AspNetCore.Http;
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
        private readonly IIncomeTypeRepo _expenseTypeRepo;
        public ExpenseTypeController(IIncomeTypeRepo expenseTypeRepo)
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
                List<IncomeTypeDTO> expenseListDto = new List<IncomeTypeDTO>();
                foreach (var expenseType in expenseTypeList)
                {
                    IncomeTypeDTO expenseTypeDto = new ExpenseTypeDTO(expenseType);
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

        [HttpGet("{expenseTypoeId}")]
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

                IncomeTypeDTO expenseTypeDto = new ExpenseTypeDTO(expenseType);
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

                IncomeTypeDTO expenseTypeDto = new ExpenseTypeDTO(expenseType);
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
                var expenseToAdd = ExpenseStaticUtils.CreateExpenseTypeFromExpenseTypeToAddDto(expenseType);
                await _expenseTypeRepo.AddExpenseType(expenseToAdd);
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
                    var expenseTypeToAdd = ExpenseStaticUtils.CreateExpenseTypeFromExpenseTypeToAddDto(expenseType);
                    expenseTypeToAddList.Add(expenseTypeToAdd);
                }
                if (expenseTypeToAddList.Count > 0)
                {
                    await _expenseTypeRepo.AddExpenseTypeList(expenseTypeToAddList);
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
                await _expenseTypeRepo.DeleteExpenseType(expenseTypeId);
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
                    await _expenseTypeRepo.DeleteExpenseType(expenseTypeId);
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
                var expenseTypeUpdated = ExpenseStaticUtils.CreateExpenseTypeFromExpenseTypeToAddDto(expenseTypeToUpdate);
                var expenseType = await _expenseTypeRepo.UpdateExpenseType(expenseTypeId, expenseTypeUpdated);
                if (expenseType is null)
                {
                    return NotFound("Nessun tipo trovato");
                }

                IncomeTypeDTO expenseDto = new ExpenseTypeDTO(expenseType);
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
