using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
