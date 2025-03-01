using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Utility.ExpenseUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.ExpenseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        private readonly IExpenseRepo _expenseRepo;
        public ExpenseCategoryController(IExpenseCategoryRepo expenseCategoryRepo,
            IExpenseTypeRepo expenseTypeRepo,             
            IExpenseRepo expenseRepo)
        {
            _expenseCategoryRepo = expenseCategoryRepo;
            _expenseTypeRepo = expenseTypeRepo;
            _expenseRepo = expenseRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive category")]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            try
            {
                var expenseCategoryList = await _expenseCategoryRepo.GetAllExpenseCategorysAsync();
                if (expenseCategoryList == null || !expenseCategoryList.Any())
                {
                    return NotFound("Nessuna category trovata");
                }
                List<ExpenseCategoryDTO> expenseListDto = new List<ExpenseCategoryDTO>();
                foreach (var expenseCategory in expenseCategoryList)
                {
                    ExpenseCategoryDTO expenseCategoryDto = ExpenseCategoryDTOConverter.ToExpenseCategoryDTO(expenseCategory);
                    expenseListDto.Add(expenseCategoryDto);
                }

                return Ok(expenseListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("categoriesAndTypes")]
        [SwaggerOperation(Summary = "Get all expensive category with assciated types")]
        public async Task<IActionResult> GetAllExpenseCategoriesAndTypes()
        {
            try
            {
                var expenseCategoryList = await _expenseCategoryRepo.GetAllExpenseCategorysAsync();
                if (expenseCategoryList == null || !expenseCategoryList.Any())
                {
                    return NotFound("Nessuna category trovata");
                }
                var expenseTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                if (expenseTypeList == null || !expenseTypeList.Any())
                {
                    return NotFound("Nessun tipo trovato");
                }
                List<ExpenseCategoryAndTypesDTO> expenseListDto = new List<ExpenseCategoryAndTypesDTO>();
                foreach (var expenseCategory in expenseCategoryList)
                {
                    ExpenseCategoryAndTypesDTO expenseCategoryDto = ExpenseCategoryDTOConverter.ToExpenseCategoryAndTypeDTO(expenseCategory);
                    expenseCategoryDto.ExpenseTypeList = expenseTypeList
                        .Where(et => et.CategoryId == expenseCategory.Id)
                        .Select(et => et.Name)
                        .ToList();
                    expenseListDto.Add(expenseCategoryDto);
                }

                return Ok(expenseListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{expenseCategoryId}")]
        [SwaggerOperation(Summary = "Get expense category by ID")]
        public async Task<IActionResult> GetExpenseCategoryById(int expenseCategoryId)
        {
            try
            {
                var expenseCategory = await _expenseCategoryRepo.GetExpenseCategoryAsync(expenseCategoryId);
                if (expenseCategory is null)
                {
                    return NotFound("Nessun category trovata.");
                }

                ExpenseCategoryDTO expenseCategoryDto = ExpenseCategoryDTOConverter.ToExpenseCategoryDTO(expenseCategory);
                return Ok(expenseCategoryDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{expenseCategoryName}/byName")]
        [SwaggerOperation(Summary = "Get expense category by name")]
        public async Task<IActionResult> GetExpenseByName(string expenseCategoryName)
        {
            try
            {
                var expenseCategory = await _expenseCategoryRepo.GetExpenseCategoryByNameAsync(expenseCategoryName);
                if (expenseCategory is null)
                {
                    return NotFound("Nessun category trovata.");
                }

                ExpenseCategoryDTO expenseCategoryDto = ExpenseCategoryDTOConverter.ToExpenseCategoryDTO(expenseCategory);
                return Ok(expenseCategoryDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive category")]
        public async Task<IActionResult> AddExpenseCategory(string expenseCategoryName)
        {
            try
            {
                var expenseCategoryToAdd = new ExpenseCategory()
                {
                    Name = expenseCategoryName,
                };
                await _expenseCategoryRepo.AddExpenseCategoryAsync(expenseCategoryToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete expense category")]
        public async Task<IActionResult> DeleteExpenseCategory(int expenseCategoryId)
        {
            try
            {
                // se ho spese con questa categoria non cancello
                var expenseList = await _expenseRepo.GetAllExpensesAsync();
                if(expenseList.Any(x=>x.ExpenseType?.Category?.Id == expenseCategoryId))
                {
                    return Conflict("Esistono asset associate a questo tipo");
                }
                await _expenseCategoryRepo.DeleteExpenseCategoryAsync(expenseCategoryId);
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

        [HttpPut("{expenseCategoryId}")]
        [SwaggerOperation(Summary = "Update expense category")]
        public async Task<IActionResult> UpdateExpenseById(int expenseCategoryId, [FromBody] ExpenseCategoryDTO expenseCategoryToUpdate)
        {
            try
            {
                var expenseCategoryUpdated = new ExpenseCategory()
                {
                    Name = expenseCategoryToUpdate.Name,
                };
                var expenseCategory = await _expenseCategoryRepo.UpdateExpenseCategoryAsync(expenseCategoryId, expenseCategoryUpdated);
                if (expenseCategory is null)
                {
                    return NotFound("Nessun category trovato");
                }

                ExpenseCategoryDTO expenseDto = ExpenseCategoryDTOConverter.ToExpenseCategoryDTO(expenseCategory);
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
