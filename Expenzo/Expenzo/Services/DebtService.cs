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
    public class DebtService : IDebtService
    {

        private readonly string debtsFilePath = Path.Combine(AppContext.BaseDirectory, "Debts.json");
        private async Task<List<Debt>> GetAllDebtsAsync()
        {
            try
            {
                if (!File.Exists(debtsFilePath))
                {
                    return new List<Debt>();
                }

                var json = await File.ReadAllTextAsync(debtsFilePath);
                return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Debt>();
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading debt: {ioEx.Message}");
                return new List<Debt>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while loading debt: {ex.Message}");
                return new List<Debt>();
            }
        }

        public async Task<Debt> GetDebtByTransactionIdAsync(int transactionId)
        {
            try
            {
                var debts = await GetAllDebtsAsync();
                // Return a single debt filtered by transactionId, or null if debts is null or no match is found
                return (debts ?? new List<Debt>()).FirstOrDefault(d => d.TransactionId == transactionId);

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving debt for transaction {transactionId}: {ex.Message}");
                return new Debt(); // Return an empty list in case of an exception
            }
        }

        public async Task<bool> DeleteDebtAsync(int debtId)
        {
            try
            {
                // Get all existing debts
                var debts = await GetAllDebtsAsync();

                // Find the debt to delete
                var debtToDelete = debts.FirstOrDefault(d => d.DebtId == debtId);
                if (debtToDelete == null)
                {
                    Console.WriteLine($"Debt with ID {debtId} not found.");
                    return false;
                }

                // Remove the debt
                debts.Remove(debtToDelete);

                // Save the updated list back to storage
                await WriteDebtsToJson(debts);

                Console.WriteLine($"Debt with ID {debtId} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting debt: {ex.Message}");
                throw;
            }
        }


        public async Task SaveDebtAsync(Debt debt)
        {
            try
            {
                var debts = await GetAllDebtsAsync();

                // transaction id
                int debtsCount = debts.Count();
                debt.DebtId = debtsCount + 1;

                debts.Add(debt);
                await WriteDebtsToJson(debts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
                throw;
            }
        }

        private async Task WriteDebtsToJson(List<Debt> debts)
        {
            try
            {
                var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(debtsFilePath, json);
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading transactions: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving transactions: {ex.Message}");
            }
        }


    }
}
