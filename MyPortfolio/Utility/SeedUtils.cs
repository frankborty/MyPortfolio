using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility.ExpenseUtils;

namespace MyPortfolio.Utility
{
    public static class SeedUtils
    {
        public static async Task SetExpenseSchema(IExpenseTypeRepo _expenseTypeRepo, IExpenseCategoryRepo _expenseCategoryRepo)
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

            await _expenseCategoryRepo.AddExpenseCategoryListAsync(expenseCategoryListToAdd);


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

            await _expenseTypeRepo.AddExpenseTypeListAsync(expenseTypesListToAdd);
        }

        internal static async Task SetIncomeSchema(IIncomeTypeRepo _incomeTypeRepo)
        {
            List<IncomeType> incomeTypeListToAdd = new List<IncomeType>()
                {
                    new IncomeType(){ Name = "Stipendio" },
                    new IncomeType(){ Name = "Rimborsi" },
                    new IncomeType(){ Name = "Regali" },
                    new IncomeType(){ Name = "StipendiAggiuntivi" },
                    new IncomeType(){ Name = "InteressiCC" },
                    new IncomeType(){ Name = "Cedole" },
                    new IncomeType(){ Name = "Altro" }
                };

            var currentTypeList = await _incomeTypeRepo.GetAllIncomeTypesAsync();
            incomeTypeListToAdd.RemoveAll(item1 => currentTypeList.Any(item2 => item2.Name == item1.Name));

            await _incomeTypeRepo.AddIncomeTypeListAsync(incomeTypeListToAdd);
        }

        internal static async Task SetAssetSchema(IAssetRepo _assetRepo, IAssetCategoryRepo _assetCategoryRepo)
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
            List<AssetCategory> categoryList = currentCategoryList.ToList();
            List<Asset> assetListToAdd = new List<Asset>() {
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Account", categoryList), Name = "PostePay" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Account", categoryList), Name = "ING" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Account", categoryList), Name = "BBVA" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Cash", categoryList), Name = "Euro" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Account", categoryList), Name = "PayPal" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Credit", categoryList), Name = "Martina" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Credit", categoryList), Name = "Virginia" },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Account", categoryList), Name = "Directa" },

                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "XZMU", ISIN = "IE00BFMNPS42", Share = 183 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "SUAS", ISIN = "IE00BYVJRR92", Share = 680 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "V3AA", ISIN = "IE00BNG8L278", Share = 1000 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "EMESG", ISIN = "LU2109787551", Share = 50 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "V3EA", ISIN = "IE000QUOSE01", Share = 432 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "V3MA", ISIN = "IE000KPJJWM6", Share = 900 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "V3PA", ISIN = "IE000GOJO2A3", Share = 392 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "M.509954", ISIN = "IT0005496994", Share = 50 },
                new  Asset(){ AssetCategory = AssetUtils.AssetStaticUtils.GetCategoryFromName("Financial", categoryList), Name = "M.510366", ISIN = "IT0005532715", Share = 50 }
            };

            var currentAssetList = await _assetRepo.GetAllAssetAsync();
            assetListToAdd.RemoveAll(item1 => currentAssetList.Any(item2 => item2.Name == item1.Name));
            await _assetRepo.AddAssetListAsync(assetListToAdd);
        }
    }
}
