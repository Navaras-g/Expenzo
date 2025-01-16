using Expenzo.Services;
using Microsoft.AspNetCore.Components;

namespace Expenzo.Components.Pages
{
    public partial class Profile
    {

        private void HandleSignout()
        {
            Nav.NavigateTo("/login");
        }
    }
}