using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.API.JWT.Interfaces;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Commands.BookingCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.BookingQueries;
using RetroArcade.Domain.Domain.Queries.BuildingQueries;
using RetroArcade.Domain.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _repo;

        public BookingsController(IBookingRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Initialise une nouvelle réservation dans la base de données.
        /// </summary>
        /// <remarks>
        /// Crée une nouvelle réservation par le joueur.
        /// </remarks>
        /// <param name="dto">Données de la nouvelle réservation du joueur.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : Réservation créée avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou réservation déjà existante.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] BookingCreateDTO dto, [FromServices] IValidator<AddBookingCommand> validator)
        {
            var command = new AddBookingCommand(
                dto.BeginDate,
                dto.EndDate,
                dto.GroupSize,
                dto.Status.ToString(),
                dto.Price,
                dto.RoomId,
                dto.AccountId
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
            return StatusCode(StatusCodes.Status201Created, "Réservation créée avec succès");
        }
        /// <summary>
        /// Récupère la liste de toutes les réservations disponibles selon une salle donnée.
        /// </summary>
        /// <returns>Une collection de réservations.</returns>
        /// <response code="200">La liste des réservations a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet("byroom/{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<Booking>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetBookingsByRoom(Guid id)
        {
            var query = new GetBookingsByRoomQuery(id);

            CqsResult<IEnumerable<Booking>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        /// <summary>
        /// Récupère le détail d'une réservation disponible selon l'identifiant de réservation donnée.
        /// </summary>
        /// <returns>Une réservation.</returns>
        /// <response code="200">La réservation a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(IEnumerable<Booking>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetBooking(Guid id)
        {
            var query = new GetBookingQuery(id);

            CqsResult<Booking> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        /// <summary>
        /// Récupère la liste de toutes les réservations disponibles selon le joueur donné.
        /// </summary>
        /// <returns>Une collection de réservations.</returns>
        /// <response code="200">La liste des réservations a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Booking>), StatusCodes.Status200OK)]
        public IActionResult GetAllBookingsByUserAccount()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userIdClaim == null || userRole == null)
                return Unauthorized("Token invalide ou incomplet.");

            Guid userId = Guid.Parse(userIdClaim);

            var query = new GetAllBookingsQuery(userId, userRole);
            CqsResult<IEnumerable<Booking>> result = _repo.Execute(query);

            if (result.IsFailure) 
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
        /// <summary>
        /// Récupère la liste de toutes les réservations disponibles selon le manager donné.
        /// </summary>
        /// <returns>Une collection de réservations.</returns>
        /// <response code="200">La liste des réservations a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet("bymanager")]
        [ProducesResponseType(typeof(IEnumerable<Booking>), StatusCodes.Status200OK)]
        public IActionResult GetAllBookingsByManagerAccount()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userIdClaim == null || userRole == null)
                return Unauthorized("Token invalide ou incomplet.");

            Guid userId = Guid.Parse(userIdClaim);

            var query = new GetAllBookingsByManagerQuery(userId, userRole);
            CqsResult<IEnumerable<Booking>> result = _repo.Execute(query);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
        /// <summary>
        /// Mets à jour une réservation dans la base de données.
        /// </summary>
        /// <remarks>
        /// Fais la mise à jour de la réservation par le joueur.
        /// </remarks>
        /// <param name="id">Identifiant de la réservation</param>
        /// <param name="dto">Données de la réservation à mettre à jour du joueur.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Réservation mis à jour avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(Guid id, [FromBody] BookingUpdateDTO dto, [FromServices] IValidator<UpdateBookingCommand> validator)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userIdClaim == null || userRole == null)
                return Unauthorized("Utilisateur non identifié.");

            var command = new UpdateBookingCommand(
                id,
                dto.GroupSize,
                Guid.Parse(userIdClaim),
                userRole
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

            return Ok("Réservation mise à jour avec succès");
        }

        /// <summary>
        /// Supprime une réservation dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la réservation</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Réservation supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteBookingCommand> validator)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userIdClaim == null) return Unauthorized();

            Guid connectedUserId = Guid.Parse(userIdClaim);

            var command = new DeleteBookingCommand(id, connectedUserId, userRole);

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

            return Ok("Réservation supprimée avec succès");
        }
    }
}
