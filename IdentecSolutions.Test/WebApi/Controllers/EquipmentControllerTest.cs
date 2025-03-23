using Castle.Core.Logging;
using IdentecSolutions.Application.Commands.Equipment.CreateEquipment;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Services.ExceptionResponseMapper;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.Domain.Exceptions;
using IdentecSolutions.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace IdentecSolutions.Test.WebApi.Controllers
{
    public class EquipmentControllerTest
    {
        private readonly Mock<IQueryDispatcher> _queryDispatcherMock;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;
        private readonly EquipmentController _controller;

        public EquipmentControllerTest()
        {
            _queryDispatcherMock = new Mock<IQueryDispatcher>();
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
            _controller = new EquipmentController(_queryDispatcherMock.Object, _commandDispatcherMock.Object);
        }

        [Fact]
        public async Task CreateEquipments_SchouldCreateEquipment()
        {
            var command = new CreateEquipmentRequest()
            {
                Name = "name eq",
                Description = "description eq",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Location = "base",
                Price = 100,
                SerialNumber = "123",
                Status = true,
                WarrantyExpiryDate = new DateTime(2025, 01, 01)
            };

            var cancellationToken = CancellationToken.None;
            var expectedResult = true;

            _commandDispatcherMock.Setup(x => x.SendAsync(command, cancellationToken)).Returns(Task.CompletedTask);

            var result = await _controller.CreateEquipment(command, cancellationToken);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        
    }
}
