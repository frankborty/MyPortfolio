using MyPortfolio.Models;
using System.Text.Json;

namespace MyPortfolio.Utility
{
    public static class GenericUtils
    {
        public static async Task<CurrentAssetPrice> GetAssetValueFromStockPriceWatcher(string url, string assetName)
        {
            string requestUrl = $"{url}?symbol={assetName}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Invia una richiesta GET all'API
                    //Console.WriteLine($"*************** {requestUrl} ***************");
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    // Verifica se la richiesta è andata a buon fine
                    if (response.IsSuccessStatusCode)
                    {
                        // Leggi la risposta come stringa
                        string responseData = await response.Content.ReadAsStringAsync();

                        var responseObject = JsonSerializer.Deserialize<CurrentAssetPrice>(responseData);
                        if(responseObject is null)
                        {
                            //Console.WriteLine($"--------- NULL --------- ");
                            throw new NullReferenceException();
                        }
                        responseObject.Price= Math.Round(responseObject.Price, 3);
                        return responseObject;
                    }
                    else
                    {
                        //Console.WriteLine($"--------- Errore durante la richiesta: {response.ReasonPhrase} --------- ");
                        throw new Exception($"Errore durante la richiesta: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"--------- Si è verificato un errore: {ex.Message} --------- ");
                    throw new Exception($"Si è verificato un errore: {ex.Message}");
                }
            }
        }
    }
}
