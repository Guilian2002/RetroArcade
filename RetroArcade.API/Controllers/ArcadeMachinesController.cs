using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.API.JWT.Interfaces;
using RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands;
using RetroArcade.Domain.Domain.Commands.RoomCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.ArcadeMachineQueries;
using RetroArcade.Domain.Domain.Queries.BookingQueries;
using RetroArcade.Domain.Domain.Queries.RoomQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/arcademachines")]
    public class ArcadeMachinesController : ControllerBase
    {
        private readonly IArcadeMachineRepository _repo;

        public ArcadeMachinesController(IArcadeMachineRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Récupère la liste de toutes les machines arcades disponibles dans le catalogue.
        /// </summary>
        /// <returns>Une collection de machines arcades disponibles dans le catalogue.</returns>
        /// <response code="200">La liste des machines arcades disponibles dans le catalogue a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArcadeMachine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var query = new GetAllArcadeMachinesQuery();

            CqsResult<IEnumerable<ArcadeMachine>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Récupère le détail d'une machine du catalogue disponible selon l'identifiant de catalogue donnée.
        /// </summary>
        /// <returns>Une machine du catalogue.</returns>
        /// <response code="200">La machine du catalogue a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ArcadeMachine), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get(Guid id)
        {
            var query = new GetArcadeMachineByIdQuery(id);

            CqsResult<ArcadeMachine> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Crée une nouvelle machine arcade disponible dans le catalogue dans la base de données.
        /// </summary>
        /// <param name="dto">Données de la nouvelle machines arcades disponible dans le catalogue par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : machine arcade disponibles dans le catalogue créée avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou machine arcade disponible dans le catalogue déjà existante.</response>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] ArcadeMachineCreateDTO dto, [FromServices] IValidator<AddArcadeMachineCommand> validator)
        {
            var command = new AddArcadeMachineCommand(
                dto.Name,
                dto.GameName,
                dto.CategorieId
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
            return StatusCode(StatusCodes.Status201Created, "Machine arcade disponible dans le catalogue créée avec succès");
        }
        /// <summary>
        /// Met à jour la machine arcade disponible dans le catalogue dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la machine arcade disponible dans le catalogue</param>
        /// <param name="dto">Données de la machine arcade disponible dans le catalogue à mettre à jour par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Machine arcade disponible dans le catalogue mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// /// <response code="401">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Manager")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(Guid id, [FromBody] ArcadeMachineUpdateDTO dto, [FromServices] IValidator<UpdateArcadeMachineCommand> validator)
        {
            var command = new UpdateArcadeMachineCommand(
                id,
                dto.Name,
                dto.GameName,
                dto.CategorieId
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

            return Ok("Machine arcade disponible dans le catalogue mis à jour avec succès");
        }
        /// <summary>
        /// Supprime une machine arcade disponible dans le catalogue dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la machine arcade disponible dans le catalogue</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Machine arcade disponible dans le catalogue supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteArcadeMachineCommand> validator)
        {

            var command = new DeleteArcadeMachineCommand(id);

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

            return Ok("Machine arcade disponible dans le catalogue supprimée avec succès");
        }
    }
}
