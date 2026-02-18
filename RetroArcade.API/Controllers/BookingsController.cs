using FluentValidation;
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
        [HttpGet("{id:Guid}")]
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
    }
}
