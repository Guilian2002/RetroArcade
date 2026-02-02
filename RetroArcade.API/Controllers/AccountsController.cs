using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.API.JWT;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly TokenManager _tokenManager;

        public AccountsController(IAccountRepository repo, TokenManager tokenManager)
        {
            _repo = repo;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Initialise un nouveau joueur dans la base de données.
        /// </summary>
        /// <remarks>
        /// Ce endpoint valide l'identité du joueur et lui assigne un rôle système.
        /// </remarks>
        /// <param name="dto">Données d'identification du joueur.</param>
        /// <response code="201">Accès accordé : Compte créé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou utilisateur déjà existant.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] AccountCreateDTO dto)
        {
            var command = new AddAccountCommand(
                dto.Firstname, 
                dto.Lastname, 
                dto.Username, 
                dto.Email, 
                dto.Password, 
                dto.Role.ToString());

            CqsResult result = _repo.Execute(command);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }
            return StatusCode(StatusCodes.Status201Created, "Compte créé avec succès");
        }

        /// <summary>
        /// Authentifie un utilisateur et génère un cookie de session sécurisé.
        /// </summary>
        /// <param name="dto">Identifiants de connexion (Login/Password).</param>
        /// <response code="200">Connexion réussie : Cookie JWT généré.</response>
        /// <response code="401">Échec d'authentification : Identifiants incorrects.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] AccountLoginDTO dto)
        {
            var command = new GetAccountByLoginQuery(
                dto.Email,
                dto.Password);

            CqsResult<Account> result = _repo.Execute(command);

            if (result.IsFailure)
                return Unauthorized("Email ou mot de passe incorrect.");

            Account account = result.Data;

            var tokenDto = new TokenAccountDTO(account.Id, account.Email, account.Username, account.Role);

            string token = _tokenManager.GenerateToken(tokenDto);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // true = https, false = http
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(1)
            });

            return Ok(new
            {
                message = "Connexion réussie",
                user = account.Username,
                role = account.Role.ToString()
            });
        }

        /// <summary>
        /// Déconnecte l'utilisateur en supprimant le cookie de session.
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Déconnexion réussie" });
        }

        /// <summary>
        /// Récupère les informations de l'utilisateur connecté à partir du cookie JWT.
        /// </summary>
        /// <response code="200">Utilisateur authentifié.</response>
        /// <response code="401">Aucun utilisateur connecté ou session expirée.</response>
        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            // Extraction des claims injectés par le middleware JWT lors de la lecture du cookie
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var username = User.FindFirst("username")?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Id = userId,
                Email = email,
                Username = username,
                Role = role
            });
        }
    }
}
