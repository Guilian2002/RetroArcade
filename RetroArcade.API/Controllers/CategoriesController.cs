using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.ArcadeMachineCommands;
using RetroArcade.Domain.Domain.Commands.CategorieCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.ArcadeMachineQueries;
using RetroArcade.Domain.Domain.Queries.CategorieQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorieRepository _repo;

        public CategoriesController(ICategorieRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Récupère la liste de toutes les categories disponibles.
        /// </summary>
        /// <returns>Une collection de categories.</returns>
        /// <response code="200">La liste des categories a été récupérée avec succès.</response>
        /// <response code="400">Une erreur est survenue lors de l'exécution de la requête.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Categorie>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var query = new GetAllCategoriesQuery();

            CqsResult<IEnumerable<Categorie>> result = _repo.Execute(query);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Crée une nouvelle categories dans la base de données.
        /// </summary>
        /// <param name="dto">Données de la nouvelle categorie par le manager.</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="201">Accès accordé : categorie créée avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides ou categorie déjà existante.</response>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CategorieCreateDTO dto, [FromServices] IValidator<AddCategorieCommand> validator)
        {
            var command = new AddCategorieCommand(
                dto.Name
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
            return StatusCode(StatusCodes.Status201Created, "Categorie créée avec succès");
        }

        /// <summary>
        /// Supprime une categorie dans la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la categorie</param>
        /// <param name="validator">Vérifie que les données sont correctes avant l'envoi</param>
        /// <response code="200">Accès accordé : Categorie supprimer avec succès.</response>
        /// <response code="400">Accès refusé : Données invalides.</response>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(Guid id, [FromServices] IValidator<DeleteCategorieCommand> validator)
        {

            var command = new DeleteCategorieCommand(id);

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

            return Ok("Categorie supprimée avec succès");
        }
    }
}
