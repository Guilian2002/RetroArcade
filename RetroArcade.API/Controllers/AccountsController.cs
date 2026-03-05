using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.API.JWT.Interfaces;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
using RetroArcade.Domain.Domain.Queries.BookingQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly ITokenManager _tokenManager;

        public AccountsController(IAccountRepository repo, ITokenManager tokenManager)
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
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : Compte créé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou utilisateur déjà existant.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] AccountCreateDTO dto, [FromServices] IValidator<AddAccountCommand> validator)
        {
            var command = new AddAccountCommand(
                dto.Firstname, 
                dto.Lastname, 
                dto.Username, 
                dto.Email, 
                dto.Password, 
                dto.Role.ToString());

            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            CqsResult result = _repo.Execute(command);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }
            return StatusCode(StatusCodes.Status201Created, "Compte créé avec succès");
        }

        /// <summary>
        /// Récupère la liste de tous les comptes disponibles.
        /// </summary>
        /// <returns>Une collection de comptes.</returns>
        /// <response code="200">La liste des comptes a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        public IActionResult GetAllAccounts()
        {
            var query = new GetAllAccountsQuery();
            CqsResult<IEnumerable<Account>> result = _repo.Execute(query);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
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

        /// <summary>
        /// Met à jour le compte dans la base de données.
        /// </summary>
        /// <remarks>
        /// Fais la mise à jour du compte par le joueur.
        /// </remarks>
        /// <param name="id">Identifiant du compte</param>
        /// <param name="dto">Données du compte à mettre à jour du joueur.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Compte mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// /// <response code="401">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(Guid id, [FromBody] AccountUpdateDTO dto, [FromServices] IValidator<UpdateAccountCommand> validator)
        {
            var command = new UpdateAccountCommand(
                id,
                dto.Firstname,
                dto.Lastname,
                dto.Username,
                dto.Role
            );

            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            CqsResult result = _repo.Execute(command);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("Compte mis à jour avec succès");
        }
        /// <summary>
        /// Supprime un compte et les comptes périmées dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant du compte</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Compte supprimé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// <response code="403">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteAccountCommand> validator)
        {
            var command = new DeleteAccountCommand(id);

            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            CqsResult result = _repo.Execute(command);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("Compte supprimée avec succès");
        }
    }
}
