using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetroArcade.API.Controllers;
using RetroArcade.API.DTOs;
using RetroArcade.API.JWT.Interfaces;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Queries.AccountQueries;
using RetroArcade.Domain.Domain.Repositories;
using Tools.Cqs.Results;
using Xunit;

namespace RetroArcade.Tests.Tests.API
{
    public class AccountsControllerTests
    {
        private readonly Mock<IAccountRepository> _repoMock;
        private readonly Mock<ITokenManager> _tokenManagerMock;
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _repoMock = new Mock<IAccountRepository>();
            _tokenManagerMock = new Mock<ITokenManager>();

            _controller = new AccountsController(_repoMock.Object, _tokenManagerMock.Object);

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void Create_Returns201_WhenCommandSucceeds()
        {
            // Arrange - Record DTO
            var dto = new AccountCreateDTO("John", "Doe", "Player1", "j@d.com", "Password123", Role.User);

            _repoMock.Setup(r => r.Execute(It.IsAny<AddAccountCommand>()))
                     .Returns(CqsResult.Success());

            // Act
            var result = _controller.Create(dto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal("Compte créé avec succès", result.Value);
        }

        [Fact]
        public void Login_ReturnsOk_AndSetsCookie_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new AccountLoginDTO("j@d.com", "Password123");
            var accountId = Guid.NewGuid();
            var fakeAccount = (Account)Activator.CreateInstance(
                typeof(Account),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                null,
                new object[] { accountId, "John", "Doe", "Player1", "j@d.com", "hashed_pwd", Role.User },
                null
            )!;

            _repoMock.Setup(r => r.Execute(It.IsAny<GetAccountByLoginQuery>()))
                     .Returns(CqsResult<Account>.Success(fakeAccount));

            _tokenManagerMock.Setup(t => t.GenerateToken(It.IsAny<TokenAccountDTO>()))
                             .Returns("fake-jwt-token");

            // Act
            var result = _controller.Login(loginDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            // Vérification du cookie dans les headers
            var setCookieHeader = _controller.Response.Headers["Set-Cookie"].ToString();
            Assert.Contains("jwt=fake-jwt-token", setCookieHeader);
            Assert.Contains("httponly", setCookieHeader.ToLower());
        }

        [Fact]
        public void Logout_ReturnsOk_AndRemovesCookie()
        {
            //Arrange

            // Act
            var result = _controller.Logout() as OkObjectResult;

            // Assert
            Assert.NotNull(result);

            Assert.Contains("jwt=;", _controller.Response.Headers["Set-Cookie"].ToString());
        }

        [Fact]
        public void Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new AccountLoginDTO("wrong@test.com", "badpassword");
            _repoMock.Setup(r => r.Execute(It.IsAny<GetAccountByLoginQuery>()))
                     .Returns(CqsResult<Account>.Failure("Email ou mot de passe incorrect."));

            // Act
            var result = _controller.Login(loginDto) as UnauthorizedObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
            Assert.Equal("Email ou mot de passe incorrect.", result.Value);
        }

        [Fact]
        public void GetCurrentUser_ReturnsUserClaims_WhenAuthenticated()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var claims = new List<System.Security.Claims.Claim>
            {
                new (System.Security.Claims.ClaimTypes.NameIdentifier, userId),
                new (System.Security.Claims.ClaimTypes.Email, "j@d.com"),
                new ("username", "Player1"),
                new (System.Security.Claims.ClaimTypes.Role, "User")
            };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "TestAuth");
            _controller.ControllerContext.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(identity);

            // Act
            var result = _controller.GetCurrentUser() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            dynamic data = result.Value!;
            Assert.Equal(userId, data.GetType().GetProperty("Id").GetValue(data, null));
            Assert.Equal("j@d.com", data.GetType().GetProperty("Email").GetValue(data, null));
        }
    }
}