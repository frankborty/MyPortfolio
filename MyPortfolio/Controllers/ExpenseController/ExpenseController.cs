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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepo _expenseRepo;
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        public ExpenseController(IExpenseRepo expenseRepo, IExpenseTypeRepo expenseTypeRepo)
        {
            _expenseRepo = expenseRepo;
            _expenseTypeRepo = expenseTypeRepo;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all expensive")]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                var expenseList = await _expenseRepo.GetAllExpensesAsync();
                if (expenseList == null || !expenseList.Any())
                {
                    return NotFound("Nessuna spesa trovata.");
                }
                List<ExpenseDTO> expenseListDto = new List<ExpenseDTO>();
                foreach (var expense in expenseList)
                {
                    ExpenseDTO expenseDto = ExpenseDTOConverter.ToExpenseDTO(expense);
                    expenseListDto.Add(expenseDto);
                }

                return Ok(expenseListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpGet("{expenseId}")]
        [SwaggerOperation(Summary = "Get expensive by ID")]
        public async Task<IActionResult> GetExpenseById(int expenseId)
        {
            try
            {
                var expense = await _expenseRepo.GetExpenseAsync(expenseId);
                if (expense is null)
                {
                    return NotFound("Nessuna spesa trovata.");
                }

                ExpenseDTO expenseDto = ExpenseDTOConverter.ToExpenseDTO(expense);
                return Ok(expenseDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive")]
        public async Task<IActionResult> AddExpense(ExpenseToAddDTO expense)
        {
            try
            {
                var expenseTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                ExpenseType? expenseType = ExpenseStaticUtils.GetTypeFromName(expense.ExpenseType, expenseTypeList.ToList());
                Expense expenseToAdd = ExpenseDTOConverter.FromExpenseToAddDTO(expense, expenseType);
                await ExpenseStaticUtils.AddSingleExpense(_expenseRepo, expenseToAdd);
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
        [SwaggerOperation(Summary = "Add expensive list")]
        public async Task<IActionResult> AddExpenseList([FromBody] List<ExpenseToAddDTO> expenseList)
        {
            try
            {
                var expenseTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                foreach (var expense in expenseList)
                {
                    ExpenseType? expenseType = ExpenseStaticUtils.GetTypeFromName(expense.ExpenseType, expenseTypeList.ToList());
                    Expense expenseToAdd = ExpenseDTOConverter.FromExpenseToAddDTO(expense, expenseType);
                    await ExpenseStaticUtils.AddSingleExpense(_expenseRepo, expenseToAdd);
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
        [SwaggerOperation(Summary = "Delete expensive")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            try
            {
                await _expenseRepo.DeleteExpenseAsync(expenseId);
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
        [SwaggerOperation(Summary = "Delete expensive list")]
        public async Task<IActionResult> DeleteExpenseList(List<int> expenseIdList)
        {
            try
            {
                foreach (var expenseId in expenseIdList)
                {
                    await _expenseRepo.DeleteExpenseAsync(expenseId);
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

        [HttpPut("{expenseId}")]
        [SwaggerOperation(Summary = "Update expensive")]
        public async Task<IActionResult> UpdateExpenseById(int expenseId, [FromBody] ExpenseToAddDTO expenseToUpdate)
        {
            try
            {
                var expenseTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                ExpenseType? expenseType = ExpenseStaticUtils.GetTypeFromName(expenseToUpdate.ExpenseType, expenseTypeList.ToList());
                Expense expenseUpdated = ExpenseDTOConverter.FromExpenseToAddDTO(expenseToUpdate, expenseType);
                var expense = await _expenseRepo.UpdateExpenseAsync(expenseId, expenseUpdated);
                if (expense is null)
                {
                    return NotFound("Nessuna spesa trovata.");
                }

                ExpenseDTO expenseDto = ExpenseDTOConverter.ToExpenseDTO(expense);
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
