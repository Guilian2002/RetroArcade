using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.RoomFeedbackCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.RoomArcadeMachineQueries;
using RetroArcade.Domain.Domain.Queries.RoomFeedbackQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/roomfeedbacks")]
    public class RoomFeedbacksController : ControllerBase
    {
        private readonly IRoomFeedbackRepository _repo;

        public RoomFeedbacksController(IRoomFeedbackRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Récupère la liste des commentaires disponibles de l'utilisateur .
        /// </summary>
        /// <returns>Une collection de commentaires.</returns>
        /// <response code="200">La liste des commentaires a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize(Roles = "User")]
        [HttpGet("byaccount/{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<RoomFeedback>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll(Guid id)
        {
            var query = new GetAllRoomFeedbacksQuery(id);

            CqsResult<IEnumerable<RoomFeedback>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Récupère la liste des commentaires disponibles par pièce .
        /// </summary>
        /// <returns>Une collection de commentaires.</returns>
        /// <response code="200">La liste des commentaires a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize(Roles = "Manager")]
        [HttpGet("byroom/{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<RoomFeedback>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllByRoom(Guid id)
        {
            var query = new GetAllRoomFeedbacksByRoomQuery(id);

            CqsResult<IEnumerable<RoomFeedback>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Crée un nouveau commentaire dans la base de données.
        /// </summary>
        /// <param name="dto">Données du nouveau commentaire.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : Commentaire créé avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou commentaire déjà existant pour la pièce.</response>
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] RoomFeedbackCreateDTO dto, [FromServices] IValidator<AddRoomFeedbackCommand> validator)
        {
            var command = new AddRoomFeedbackCommand(
                dto.Stars,
                dto.Comment,
                dto.AccountId,
                dto.RoomId
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
            return StatusCode(StatusCodes.Status201Created, "Commentaire créé avec succès");
        }
        /// <summary>
        /// Supprime un commentaire dans la base de données.
        /// </summary>
        /// <param name="roomId">Identifiant du commentaire</param>
        /// <param name="arcadeMachineId">Identifiant du commentaire</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Commentaire supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "User")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteRoomFeedbackCommand> validator)
        {

            var command = new DeleteRoomFeedbackCommand(id);

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

            return Ok("Commentaire supprimé avec succès");
        }
    }
}
