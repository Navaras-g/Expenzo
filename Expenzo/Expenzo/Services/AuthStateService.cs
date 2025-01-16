using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;

namespace Expenzo.Services
{
    public class AuthStateService
    {

        private User loggedInUser;

        public User GetLoggedInUser()
        {
            return loggedInUser;
        }

        public void SetLoggedInUser(User user)
        {
            loggedInUser = user;
        }

        public bool IsLoggedIn()
        {
            if (loggedInUser != null)
            {
                return true;
            }
            return false;
        }

        public void Signout()
        {
            loggedInUser = null;
        }
    }
}
