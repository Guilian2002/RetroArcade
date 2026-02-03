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

            if (context.Controller is Controller controller)
            {
                controller.ViewBag.CurrentUser = user;
            }

            if (user != null)
            {
                var action = context.RouteData.Values["action"]?.ToString();
                var controllerName = context.RouteData.Values["controller"]?.ToString();

                if (controllerName == "Account" && (action == "Login" || action == "Create"))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }
            }

            await next();
        }
    }
}
