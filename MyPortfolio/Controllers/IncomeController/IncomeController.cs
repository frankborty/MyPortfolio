using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.DTO.IncomeDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Utility.ExpenseUtils;
using MyPortfolio.Utility.IncomeUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers.IncomeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepo _incomeRepo;
        public IncomeController(IIncomeRepo incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get all incomes")]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                var incomeList = await _incomeRepo.GetAllIncomesAsync();
                if (incomeList == null || !incomeList.Any())
                {
                    return NotFound("Nessun income trovata.");
                }
                List<IncomeDTO> incomeListDto = new List<IncomeDTO>();
                foreach (var income in incomeList)
                {
                    IncomeDTO incomeDto = IncomeDTOConverter.ToIncomeDTO(income);
                    incomeListDto.Add(incomeDto);
                }

                return Ok(incomeListDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
        
        [HttpGet("{incomeId}")]
        [SwaggerOperation(Summary = "Get income by ID")]
        public async Task<IActionResult> GetIncomeById(int incomeId)
        {
            try
            {
                var income = await _incomeRepo.GetIncomeAsync(incomeId);
                if (income is null)
                {
                    return NotFound("Nessun income trovata.");
                }

                IncomeDTO incomeDto = IncomeDTOConverter.ToIncomeDTO(income);
                return Ok(incomeDto);
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add income")]
        public async Task<IActionResult> AddIncome(IncomeDTO income)
        {
            try
            {
                var incomeToAdd = IncomeDTOConverter.FromIncomeDTO(income);
                await IncomeStaticUtils.AddSingleIncome(_incomeRepo, incomeToAdd);
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
        [SwaggerOperation(Summary = "Add income list")]
        public async Task<IActionResult> AddIncomeList([FromBody] List<IncomeDTO> incomeList)
        {
            try
            {
                foreach (var income in incomeList)
                {
                    var incomeToAdd = IncomeDTOConverter.FromIncomeDTO(income);
                    await IncomeStaticUtils.AddSingleIncome(_incomeRepo, incomeToAdd);
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
        [SwaggerOperation(Summary = "Delete income")]
        public async Task<IActionResult> DeleteIncome(int incomeId)
        {
            try
            {
                await _incomeRepo.DeleteIncomeAsync(incomeId);
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
        [SwaggerOperation(Summary = "Delete income list")]
        public async Task<IActionResult> DeleteIncomeList(List<int> incomeIdList)
        {
            try
            {
                foreach (var incomeId in incomeIdList)
                {
                    await _incomeRepo.DeleteIncomeAsync(incomeId);
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

        [HttpPut("{incomeId}")]
        [SwaggerOperation(Summary = "Update income")]
        public async Task<IActionResult> UpdateIncomeById(int incomeId, [FromBody] IncomeDTO incomeToUpdate)
        {
            try
            {
                var incomeUpdated = IncomeDTOConverter.FromIncomeDTO(incomeToUpdate);
                var income = await _incomeRepo.UpdateIncomeAsync(incomeId, incomeUpdated);
                if (income is null)
                {
                    return NotFound("Nessuna spesa trovata.");
                }

                IncomeDTO incomeDto = IncomeDTOConverter.ToIncomeDTO(income);
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
