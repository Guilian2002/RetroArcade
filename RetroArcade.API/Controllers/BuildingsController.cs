using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/buildings")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingRepository _repo;

        public BuildingsController(IBuildingRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Récupère la liste de tous les bâtiments disponibles.
        /// </summary>
        /// <returns>Une collection de bâtiments.</returns>
        /// <response code="200">La liste des bâtiments a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Building>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var query = new GetAllBuildingsQuery();

            CqsResult<IEnumerable<Building>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Récupère les détails d'un bâtiment spécifique par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant unique (GUID) du bâtiment.</param>
        /// <returns>Les informations détaillées du bâtiment.</returns>
        /// <response code="200">Bâtiment trouvé et retourné avec succès.</response>
        /// <response code="400">L'identifiant fourni est invalide ou une erreur de traitement est survenue.</response>
        /// <response code="404">Aucun bâtiment trouvé pour cet identifiant.</response>
        [Authorize]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var query = new GetBuildingByIdQuery(id);

            CqsResult<Building> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        /// <summary>
        /// Crée un nouveau bâtiment dans la base de données.
        /// </summary>
        /// <param name="dto">Données du nouveau bâtiment par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : bâtiment créé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou bâtiment déjà existant.</response>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] BuildingCreateDTO dto, [FromServices] IValidator<AddBuildingCommand> validator)
        {
            var command = new AddBuildingCommand(
                dto.Name,
                dto.OpeningHour,
                dto.ClosingHour,
                dto.AddressStreet,
                dto.AddressNumber,
                dto.PostalCode,
                dto.City,
                dto.Country,
                dto.ManagerId
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
            return StatusCode(StatusCodes.Status201Created, "Bâtiment créé avec succès");
        }
        /// <summary>
        /// Met à jour le bâtiment dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant du bâtiment</param>
        /// <param name="dto">Données du bâtiment à mettre à jour du manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Bâtiment mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// /// <response code="401">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Manager")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(Guid id, [FromBody] BuildingUpdateDTO dto, [FromServices] IValidator<UpdateBuildingCommand> validator)
        {
            var command = new UpdateBuildingCommand(
                id,
                dto.Name,
                dto.OpeningHour,
                dto.ClosingHour,
                dto.AddressStreet,
                dto.AddressNumber,
                dto.PostalCode,
                dto.City,
                dto.Country,
                dto.ManagerId
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

            return Ok("Bâtiment mis à jour avec succès");
        }
        /// <summary>
        /// Supprime un bâtiment dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant du bâtiment</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Bâtiment supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteBuildingCommand> validator)
        {

            var command = new DeleteBuildingCommand(id);

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

            return Ok("Bâtiment supprimé avec succès");
        }
    }
}
