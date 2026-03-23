using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using RetroArcade.Domain.Domain.Commands.RoomCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Queries.RoomQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _repo;

        public RoomsController(IRoomRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Récupère la liste de toutes les pièces disponibles présentes dans un bâtiment.
        /// </summary>
        /// <returns>Une collection de pièces.</returns>
        /// <response code="200">La liste des pièces a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<Room>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll(Guid id)
        {
            var query = new GetAllRoomsQuery(id);

            CqsResult<IEnumerable<Room>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Récupère les informations complètes d'une salle d'arcade, incluant ses machines installées.
        /// </summary>
        /// <param name="id">L'identifiant unique (GUID) de la salle.</param>
        /// <returns>Une pièce contenant les détails de la salle, du bâtiment et la liste des machines.</returns>
        /// <response code="200">La salle a été trouvée et les détails sont retournés.</response>
        /// <response code="400">Erreur lors de l'exécution de la requête (ex: format GUID invalide).</response>
        /// <response code="404">Aucune salle ne correspond à l'identifiant fourni.</response>
        [Authorize]
        [HttpGet("details/{id:Guid}")]
        [ProducesResponseType(typeof(Room), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var query = new GetRoomByIdQuery(id);

            CqsResult<Room> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Crée une nouvelle pièce dans la base de données.
        /// </summary>
        /// <param name="dto">Données de la nouvelle pièce par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : pièce créée avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou pièce déjà existante.</response>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] RoomCreateDTO dto, [FromServices] IValidator<AddRoomCommand> validator)
        {
            var command = new AddRoomCommand(
                dto.Name,
                dto.Number,
                dto.MachineCapacity,
                dto.Price,
                dto.BuildingId
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
            return StatusCode(StatusCodes.Status201Created, "Pièce créée avec succès");
        }
        /// <summary>
        /// Met à jour la pièce dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la pièce</param>
        /// <param name="dto">Données de la pièce à mettre à jour par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Pièce mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// /// <response code="401">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Manager")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(Guid id, [FromBody] RoomUpdateDTO dto, [FromServices] IValidator<UpdateRoomCommand> validator)
        {
            var command = new UpdateRoomCommand(
                id,
                dto.Name,
                dto.Number,
                dto.MachineCapacity,
                dto.Price,
                dto.BuildingId
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

            return Ok("Pièce mis à jour avec succès");
        }
        /// <summary>
        /// Supprime une pièce dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la pièce</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Pièce supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteRoomCommand> validator)
        {

            var command = new DeleteRoomCommand(id);

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

            return Ok("Pièce supprimé avec succès");
        }
    }
}
