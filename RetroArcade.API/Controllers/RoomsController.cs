using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
