using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace RetroArcade.BlazorWebAssembly.Pages
{
    public partial class Home : ComponentBase
    {
        protected string GetRoleClass(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin")) return "role-admin";
            if (user.IsInRole("Manager")) return "role-manager";
            return "role-user";
        }

        protected string GetRoleName(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin")) return "ADMIN_SYS_MASTER";
            if (user.IsInRole("Manager")) return "CENTRE_MANAGER";
            return "RECRUE_JOUEUR";
        }
    }
}
