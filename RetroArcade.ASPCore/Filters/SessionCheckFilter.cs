using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RetroArcade.ASPCore.Clients;
using System.Threading.Tasks;

namespace RetroArcade.ASPCore.Filters
{
    public class SessionCheckFilter : IAsyncActionFilter
    {
        private readonly AuthentificationAPIClient _auth;

        public SessionCheckFilter(AuthentificationAPIClient auth)
        {
            _auth = auth;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await _auth.GetMeAsync();
            var controllerName = context.RouteData.Values["controller"]?.ToString();

            if (context.Controller is Controller controller)
            {
                controller.ViewBag.CurrentUser = user;
            }

            if (user != null)
            {
                var action = context.RouteData.Values["action"]?.ToString();

                if (controllerName == "Account" && (action == "Login" || action == "Create"))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }
            }

            if (controllerName == "Building")
            {
                if (user == null || (user.Role != "Admin" && user.Role != "User"))
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                    return;
                }
            }

            await next();
        }
    }
}
