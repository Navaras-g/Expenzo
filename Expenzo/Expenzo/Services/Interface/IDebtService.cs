using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services.Interface
{
    public interface IDebtService
    {
        Task SaveDebtAsync(Debt debt);
        Task<bool> DeleteDebtAsync(int debtId);
        Task<Debt> GetDebtByTransactionIdAsync(int transactionId);

        //Task<User> GetUserAsync(string username);
    }
}
