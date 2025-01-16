using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Expenzo.Model;
using Expenzo.Services.Interface;

namespace Expenzo.Services
{
    public class CurrencyService : ICurrencyService
    {

        private readonly string currenciesFilePath = Path.Combine(AppContext.BaseDirectory, "Currencies.json");

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            try
            {
                if (!File.Exists(currenciesFilePath))
                {
                    return new List<Currency>();
                }

                var json = await File.ReadAllTextAsync(currenciesFilePath);
                return JsonSerializer.Deserialize<List<Currency>>(json) ?? new List<Currency>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Currency>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading currencies: {ioEx.Message}");
                return new List<Currency>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading currencies: {ex.Message}");
                return new List<Currency>();
            }
        }

        public async Task SaveCurrencyAsync(Currency currency)
        {
            try
            {
                var currencies = await GetAllCurrenciesAsync();

                currencies.Add(currency);
                await WriteCurrenciesToJson(currencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving category: {ex.Message}");
                //throw;
            }
        }

        private async Task WriteCurrenciesToJson(List<Currency> currencies)
        {
            try
            {
                var json = JsonSerializer.Serialize(currencies, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(currenciesFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading currencies: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving currencies: {ex.Message}");
                //throw;
            }
        }
    }
}
