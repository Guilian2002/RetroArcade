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
    }
}
