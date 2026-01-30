using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetroArcade.API.Controllers;
using RetroArcade.API.DTOs;
using RetroArcade.Domain.Domain.Commands.AccountCommands;
using RetroArcade.Domain.Domain.Entities;
using RetroArcade.Domain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Cqs.Results;
using Xunit;

namespace RetroArcade.Tests.Tests.API
{
    public class AccountsControllerTests
    {
        private readonly Mock<IAccountRepository> _repoMock;
        private readonly AccountsController _controller;

        public AccountsControllerTests()
        {
            _repoMock = new Mock<IAccountRepository>();
            _controller = new AccountsController(_repoMock.Object);
        }

        [Fact]
        public void Create_Returns201_WhenCommandSucceeds()
        {
            // Arrange
            AccountCreateDTO dto = new AccountCreateDTO
            (
                "John",
                "Doe",
                "Player1",
                "j@d.com",
                "Password123",
                Role.User
            );

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
        public void Create_ReturnsBadRequest_WhenCommandFails()
        {
            // Arrange
            var dto = new AccountCreateDTO(
                "John",
                "Doe",
                "Player1",
                "j@d.com",
                "P123",
                Role.User
            );
            string errorMsg = "Erreur lors de la création du compte";

            _repoMock.Setup(r => r.Execute(It.IsAny<AddAccountCommand>()))
                     .Returns(CqsResult.Failure(errorMsg));

            // Act
            var result = _controller.Create(dto) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(errorMsg, result.Value);
        }
    }
}
