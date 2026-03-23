using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Models.Authentification;
using System.Text.Json;
using System.Threading.Tasks;

namespace RetroArcade.ASPCore.Filters
{
    public class SessionCheckFilter : IAsyncActionFilter
    {
        private readonly AuthentificationAPIClient _auth;
        private const string SessionKey = "SessionAccount";

        public SessionCheckFilter(AuthentificationAPIClient auth)
        {
            _auth = auth;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var session = context.HttpContext.Session;
            AccountInfo? account = null;


            var userJson = session.GetString(SessionKey);

            if (!string.IsNullOrEmpty(userJson))
            {
                try
                {
                    account = JsonSerializer.Deserialize<AccountInfo>(userJson);
                }
                catch
                {
                    session.Remove(SessionKey);
                }
            }

            if (account == null)
            {
                account = await _auth.GetMeAsync();
                if (account != null)
                {
                    session.SetString(SessionKey, JsonSerializer.Serialize(account));
                }
            }

            if (context.Controller is Controller controller)
            {
                controller.ViewBag.CurrentUser = account;
            }

            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();

            if (account != null && controllerName == "Account" && 
                (actionName == "Login" || actionName == "Create"))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }

            if (controllerName == "Building" || controllerName == "Room")
            {
                if (account == null || (account.Role != "Admin" && account.Role != "User"))
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }
            }

            await next();
        }
    }
}
