using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services.Interface
{
    public interface ICurrencyService
    {
        Task SaveCurrencyAsync(Currency currency);

        Task<List<Currency>> GetAllCurrenciesAsync();
    }
}
