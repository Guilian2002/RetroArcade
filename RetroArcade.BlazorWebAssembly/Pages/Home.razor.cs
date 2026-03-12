using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RetroArcade.BlazorWebAssembly.Pages
{
    public partial class Home
    {
        protected string GetRoleClass(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin")) return "role-admin";
            if (user.IsInRole("Manager")) return "role-manager";
            return "role-user";
        }

        protected string GetRoleName(ClaimsPrincipal user)
        {
            if (user.IsInRole("Admin")) return "ADMINISTRATEUR_SYSTÈME";
            if (user.IsInRole("Manager")) return "MANAGER";
            return "UTILISATEUR_STANDARD";
        }
    }
}