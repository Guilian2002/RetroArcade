using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Models.Authentification;
using System.Reflection;

namespace RetroArcade.ASPCore.Controllers.Authentification
{
    public class AccountController : Controller
    {
        private readonly AuthentificationAPIClient _auth;

        public AccountController(AuthentificationAPIClient auth)
        {
            _auth = auth;
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccountViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                bool response = await _auth.AccountCreateAsync(
                    model.Firstname,
                    model.Lastname,
                    model.Username,
                    model.Email,
                    model.Password
                );

                if (response)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création.");
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
