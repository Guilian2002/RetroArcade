using Microsoft.AspNetCore.Mvc;
using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Models.Authentification;

namespace RetroArcade.ASPCore.Controllers.Authentification
{
    public class AccountController : Controller
    {
        private readonly AuthentificationAPIClient _auth;

        public AccountController(AuthentificationAPIClient auth)
        {
            _auth = auth;
        }

        // GET: AccountController/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            bool success = await _auth.LoginAsync(model.Email, model.Password);

            if (success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
            return View(model);
        }

        // GET: AccountController/Create
        [HttpGet]
        public IActionResult Create()
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

        // POST: AccountController/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}