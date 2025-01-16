using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services.Interface
{
    public interface ITransactionService
    {
        Task<int> SaveTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetUsersTransactionsAsync(int userId);
        Task<bool> DeleteTransactionAsync(int transactionId);

        //Task<User> GetUserAsync(string username);
    }
}
