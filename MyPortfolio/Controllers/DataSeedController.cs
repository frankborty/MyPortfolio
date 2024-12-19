using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility.AssetUtils;
using MyPortfolio.Utility.ExpenseUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedController : ControllerBase
    {
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        private readonly IAssetTypeRepo _assetTypeRepo;
        private readonly IAssetCategoryRepo _assetCategoryRepo;
        private readonly IIncomeTypeRepo _incomeTypeRepo;
        public DataSeedController(IExpenseTypeRepo expenseTypeRepo,
            IExpenseCategoryRepo expenseCategoryRepo,
            IAssetTypeRepo assetTypeRepo,
            IAssetCategoryRepo assetCategoryRepo,
            IIncomeTypeRepo incomeTypeRepo)
        {
            _expenseTypeRepo = expenseTypeRepo;
            _expenseCategoryRepo = expenseCategoryRepo;
            _assetTypeRepo = assetTypeRepo;
            _assetCategoryRepo = assetCategoryRepo;
            _incomeTypeRepo = incomeTypeRepo;
        }

        [HttpPost]
        [Route("expenseSchema")]
        [SwaggerOperation(Summary = "Seed expense schema")]
        public async Task<IActionResult> PostExpenseSchema()
        {
            try
            {
                List<ExpenseCategory> expenseCategoryListToAdd = new List<ExpenseCategory>()
                {
                    new ExpenseCategory(){ Name = "Casa" },
                    new ExpenseCategory(){ Name = "Salute" },
                    new ExpenseCategory(){ Name = "Spesa" },
                    new ExpenseCategory(){ Name = "Auto" },
                    new ExpenseCategory(){ Name = "Fun" },
                    new ExpenseCategory(){ Name = "Shop" },
                    new ExpenseCategory(){ Name = "Vesiti" },
                    new ExpenseCategory(){ Name = "Regali" },
                    new ExpenseCategory(){ Name = "Fees" },
                    new ExpenseCategory(){ Name = "AltreSpese" },
                };

                var currentCategoryList = await _expenseCategoryRepo.GetAllExpenseCategorysAsync();
                expenseCategoryListToAdd.RemoveAll(item1 => currentCategoryList.Any(item2 => item2.Name == item1.Name));

                await _expenseCategoryRepo.AddExpenseCategoryList(expenseCategoryListToAdd);


                currentCategoryList = await _expenseCategoryRepo.GetAllExpenseCategorysAsync();
                if (currentCategoryList is null)
                {
                    throw new Exception("Current category list is null");
                }

                List<ExpenseType> expenseTypesListToAdd = new List<ExpenseType>()
                {
                    new ExpenseType(){ Name = "Mamma", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ElettMamma", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ElettNonnaMaria", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ElettNonnaAlba", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Gas", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Internet", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Telefono", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AltroPerCasa", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Casa", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "Medicine", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Dottori", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Occhiali", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Lenti", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Capelli", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AltroSalute", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Salute", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "Conad", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Spesa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "EuroSpin", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Spesa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Lidl", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Spesa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Crai", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Spesa", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AltroSpesa", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Spesa", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "Rata", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Rifornimento", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Assicurazione", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Bollo", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Revisione", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Meccanico", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Gomme", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AltroAuto", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Auto", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "Ristoranti", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Bar", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Giochi", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "CeneVarie", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Feste", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Mare", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ExtraFun", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Viaggi", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fun", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "TechGadget", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Shop", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AltriGadget", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Shop", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "Vestiti", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Shop", currentCategoryList.ToList())!.Id },


                    new ExpenseType(){ Name = "OtherRegali", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Regali", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "Postepay", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fees", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "AmazonPrime", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fees", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ContoTitoli", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fees", currentCategoryList.ToList())!.Id },
                    new ExpenseType(){ Name = "ContoCorrente", CategoryId = ExpenseStaticUtils.GetCategoryFromName("Fees", currentCategoryList.ToList())!.Id },

                    new ExpenseType(){ Name = "SpeseVarie", CategoryId = ExpenseStaticUtils.GetCategoryFromName("AltreSpese", currentCategoryList.ToList())!.Id }
                };

                var currentTypeList = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                expenseTypesListToAdd.RemoveAll(item1 => currentTypeList.Any(item2 => item2.Name == item1.Name));

                await _expenseTypeRepo.AddExpenseTypeList(expenseTypesListToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("incomeSchema")]
        [SwaggerOperation(Summary = "Seed income schema")]
        public async Task<IActionResult> PostIncomeSchema()
        {
            try
            {
                List<IncomeType> incomeTypeListToAdd = new List<IncomeType>()
                {
                    new IncomeType(){ Name = "Stipendio" },
                    new IncomeType(){ Name = "Rimborsi" },
                    new IncomeType(){ Name = "Regali" },
                    new IncomeType(){ Name = "StipendiAggiuntivi" },
                    new IncomeType(){ Name = "InteressiCC" },
                    new IncomeType(){ Name = "Altro" }
                };

                var currentTypeList = await _incomeTypeRepo.GetAllIncomeTypesAsync();
                incomeTypeListToAdd.RemoveAll(item1 => currentTypeList.Any(item2 => item2.Name == item1.Name));

                await _incomeTypeRepo.AddIncomeTypeList(incomeTypeListToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("assetSchema")]
        [SwaggerOperation(Summary = "Seed asset schema")]
        public async Task<IActionResult> PostAssetSchema()
        {
            try
            {
                List<AssetCategory> assetCategoryListToAdd = new List<AssetCategory>()
                {
                    new AssetCategory(){ Name = "Account" },
                    new AssetCategory(){ Name = "Cash" },
                    new AssetCategory(){ Name = "Credit" },
                    new AssetCategory(){ Name = "Financial", IsInvested=true }
                };

                var currentCategoryList = await _assetCategoryRepo.GetAllAssetCategoryAsync();
                assetCategoryListToAdd.RemoveAll(item1 => currentCategoryList.Any(item2 => item2.Name == item1.Name));

                await _assetCategoryRepo.AddAssetCategoryListAsync(assetCategoryListToAdd);


                currentCategoryList = await _assetCategoryRepo.GetAllAssetCategoryAsync();
                if (currentCategoryList is null)
                {
                    throw new Exception("Current category list is null");
                }

                List<AssetType> assetTypesListToAdd = new List<AssetType>()
                {
                    new AssetType(){ Name = "PostePay", CategoryId = AssetStaticUtils.GetCategoryFromName("Account", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "PayPal", CategoryId = AssetStaticUtils.GetCategoryFromName("Account", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "BBVA", CategoryId = AssetStaticUtils.GetCategoryFromName("Account", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "ING", CategoryId = AssetStaticUtils.GetCategoryFromName("Account", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "Directa", CategoryId = AssetStaticUtils.GetCategoryFromName("Account", currentCategoryList.ToList())!.Id },

                    new AssetType(){ Name = "Euro", CategoryId = AssetStaticUtils.GetCategoryFromName("Cash", currentCategoryList.ToList())!.Id },

                    new AssetType(){ Name = "Martina", CategoryId = AssetStaticUtils.GetCategoryFromName("Credit", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "Francesca", CategoryId = AssetStaticUtils.GetCategoryFromName("Credit", currentCategoryList.ToList())!.Id },

                    new AssetType(){ Name = "ETF", CategoryId = AssetStaticUtils.GetCategoryFromName("Financial", currentCategoryList.ToList())!.Id },
                    new AssetType(){ Name = "BTP", CategoryId = AssetStaticUtils.GetCategoryFromName("Financial", currentCategoryList.ToList())!.Id }

                };

                var currentTypeList = await _assetTypeRepo.GetAllAssetTypeAsync();
                assetTypesListToAdd.RemoveAll(item1 => currentTypeList.Any(item2 => item2.Name == item1.Name));

                await _assetTypeRepo.AddAssetTypeListAsync(assetTypesListToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
