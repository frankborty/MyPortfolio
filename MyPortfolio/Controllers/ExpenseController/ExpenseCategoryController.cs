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
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        public ExpenseCategoryController(IExpenseCategoryRepo expenseCategoryRepo)
        {
            _expenseCategoryRepo = expenseCategoryRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive category")]
        public async Task<IActionResult> GetAllExpenseCategorys()
        {
            try
            {
                var expenseCategoryList = await _expenseCategoryRepo.GetAllExpenseCategorysAsync();
                if (expenseCategoryList == null || !expenseCategoryList.Any())
                {
                    return NotFound("Nessun tipo trovato");
                }
                List<ExpenseCategoryDTO> expenseListDto = new List<ExpenseCategoryDTO>();
                foreach (var expenseCategory in expenseCategoryList)
                {
                    ExpenseCategoryDTO expenseCategoryDto = new ExpenseCategoryDTO(expenseCategory);
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

        [HttpGet("{expenseTypoeId}")]
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

                ExpenseCategoryDTO expenseCategoryDto = new ExpenseCategoryDTO(expenseCategory);
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

                ExpenseCategoryDTO expenseCategoryDto = new ExpenseCategoryDTO(expenseCategory);
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
                await _expenseCategoryRepo.AddExpenseCategory(expenseCategoryToAdd);
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
        [SwaggerOperation(Summary = "Add expensive category list")]
        public async Task<IActionResult> AddExpenseCategoryList([FromBody] List<string> expenseCategoryNameList)
        {
            try
            {
                List<ExpenseCategory> expenseCategoryToAddList = new List<ExpenseCategory>();
                foreach (var expenseCategoryName in expenseCategoryNameList)
                {
                    var expenseCategoryToAdd = new ExpenseCategory()
                    {
                        Name = expenseCategoryName,
                    };
                    expenseCategoryToAddList.Add(expenseCategoryToAdd);
                }
                if (expenseCategoryToAddList.Count > 0)
                {
                    await _expenseCategoryRepo.AddExpenseCategoryList(expenseCategoryToAddList);
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
        [SwaggerOperation(Summary = "Delete expense category")]
        public async Task<IActionResult> DeleteExpenseCategory(int expenseCategoryId)
        {
            try
            {
                await _expenseCategoryRepo.DeleteExpenseCategory(expenseCategoryId);
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
        [SwaggerOperation(Summary = "Delete expensive category list")]
        public async Task<IActionResult> DeleteExpenseCategoryList(List<int> expenseCategoryIdList)
        {
            try
            {
                foreach (var expenseCategoryId in expenseCategoryIdList)
                {
                    await _expenseCategoryRepo.DeleteExpenseCategory(expenseCategoryId);
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

        [HttpPut("{expenseCategoryId}")]
        [SwaggerOperation(Summary = "Update expense category")]
        public async Task<IActionResult> UpdateExpenseById(int expenseCategoryId, string newExpenseCategoryName)
        {
            try
            {
                var expenseCategoryUpdated = new ExpenseCategory()
                {
                    Name = newExpenseCategoryName,
                };
                var expenseCategory = await _expenseCategoryRepo.UpdateExpenseCategory(expenseCategoryId, expenseCategoryUpdated);
                if (expenseCategory is null)
                {
                    return NotFound("Nessun category trovato");
                }

                ExpenseCategoryDTO expenseDto = new ExpenseCategoryDTO(expenseCategory);
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
