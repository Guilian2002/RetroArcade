using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;

namespace RetroArcade.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _repo;
        public AccountsController(IAccountRepository repo) => _repo = repo;

        [HttpPost]
        public IActionResult Create([FromBody] AccountCreateDTO dto)
        {
            var command = new AddAccountCommand(
                dto.Firstname, 
                dto.Lastname, 
                dto.Username, 
                dto.Email, 
                dto.Password, 
                dto.Role);

            CqsResult result = _repo.Execute(command);

            if (result.IsFailure)
            {
                return BadRequest(result.ErrorMessage);
            }
            return StatusCode(StatusCodes.Status201Created, "Film créé avec succès");
        }
    }
}
