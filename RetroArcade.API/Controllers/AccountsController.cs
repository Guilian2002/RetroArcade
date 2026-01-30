using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _repo;
        public AccountsController(IAccountRepository repo) => _repo = repo;

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
    }
}
