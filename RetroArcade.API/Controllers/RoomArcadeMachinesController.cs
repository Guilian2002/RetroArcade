using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.BuildingCommands;
using RetroArcade.Domain.Domain.Commands.RoomArcadeMachineCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Queries.RoomArcadeMachineQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/roomarcademachines")]
    public class RoomArcadeMachinesController : ControllerBase
    {
        private readonly IRoomArcadeMachineRepository _repo;

        public RoomArcadeMachinesController(IRoomArcadeMachineRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Récupère la liste de toutes les machines d'arcades des pièces disponibles.
        /// </summary>
        /// <returns>Une collection de machines d'arcades.</returns>
        /// <response code="200">La liste des machines d'arcades a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoomArcadeMachine>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var query = new GetAllRoomArcadeMachinesQuery();

            CqsResult<IEnumerable<RoomArcadeMachine>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Crée une nouvelle machine d'arcade dans une pièce dans la base de données.
        /// </summary>
        /// <param name="dto">Données de la nouvelle machine d'arcade par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : Machine d'arcade créé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou machine d'arcade déjà existant dans la pièce.</response>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] RoomArcadeMachineCreateDTO dto, [FromServices] IValidator<AddRoomArcadeMachineCommand> validator)
        {
            var command = new AddRoomArcadeMachineCommand(
                dto.RoomId, 
                dto.ArcadeMachineId,
                dto.State,
                dto.InstallationDate
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
            return StatusCode(StatusCodes.Status201Created, "Machine d'arcade dans une pièce créé avec succès");
        }
        /// <summary>
        /// Met à jour la machine d'arcade dans une pièce dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la machine d'arcade</param>
        /// <param name="dto">Données de la machine d'arcade dans une pièce à mettre à jour du manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Machine d'arcade dans une pièce mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        /// /// <response code="401">Accès refusé : Utilisateur non identifié.</response>
        [Authorize(Roles = "Manager")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update([FromBody] RoomArcadeMachineUpdateDTO dto, [FromServices] IValidator<UpdateRoomArcadeMachineCommand> validator)
        {
            var command = new UpdateRoomArcadeMachineCommand(
                dto.RoomId,
                dto.ArcadeMachineId,
                dto.State,
                dto.InstallationDate
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

            return Ok("Machine d'arcade dans une pièce mis à jour avec succès");
        }
        /// <summary>
        /// Supprime une machine d'arcade dans une pièce dans la base de données.
        /// </summary>
        /// <param name="roomId">Identifiant de la pièce</param>
        /// <param name="arcadeMachineId">Identifiant de la machine d'arcade du catalogue</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Machine d'arcade supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{roomId:Guid}/{arcadeMachineId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid roomId, Guid arcadeMachineId, 
            [FromServices] IValidator<DeleteRoomArcadeMachineCommand> validator)
        {

            var command = new DeleteRoomArcadeMachineCommand(roomId, arcadeMachineId);

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

            return Ok("Machine d'arcade dans une pièce supprimé avec succès");
        }
    }
}
