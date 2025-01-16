using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services.Interface
{
    public interface IUserService
    {
        Task SaveUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();

        //Task<User> GetUserAsync(string username);
    }
}
