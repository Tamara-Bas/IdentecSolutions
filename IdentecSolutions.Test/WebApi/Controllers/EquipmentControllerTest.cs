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
        public async Task CreateEquipments_SchouldCreateEquipment_StatusCode200()
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

        [Fact]
        public async Task CreateEquipments_SchouldNotCreateEquipment_StatusCode409()
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
            var expectedResult = false;

            _commandDispatcherMock
                  .Setup(x => x.SendAsync(command,cancellationToken))
                  .Returns(Task.FromException<ConflictException>(new ConflictException("Equipment already exists")));
            var result = await _controller.CreateEquipment(command, cancellationToken);

            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(expectedResult, conflictResult.Value);
            Assert.Equal(StatusCodes.Status409Conflict, conflictResult.StatusCode);
        }

        //[Fact]
        //public async Task CreateEquipment_ReturnsConflict_WhenConflictExceptionIsThrown()
        //{
        //    // Arrange
        //    var mockCommandDispatcher = new Mock<ICommandDispatcher>();
        //    var request = new CreateEquipmentRequest
        //    {
        //        Name = "Existing Equipment"
        //    };

        //    // Simulate a conflict by throwing a ConflictException
        //    mockCommandDispatcher
        //        .Setup(x => x.SendAsync(It.IsAny<CreateEquipmentRequest>(), It.IsAny<CancellationToken>()))
        //        .ThrowsAsync(new ConflictException("Equipment already exists"));

        //   // var controller = new YourController(mockCommandDispatcher.Object);

        //    // Act
        //    //var result;

        //    var middleware = new ExceptionHandlingMiddleware((innerHttpContext) =>
        //    {
        //        return _controller.CreateEquipment(request, CancellationToken.None);
        //    });

        //    // Simulate the middleware
        //    var context = new DefaultHttpContext();
        //  //var logger = new ILogger<ExceptionHandlingMiddleware>();
        ////    var te
        //    await middleware.InvokeAsync(context, null,null);

        //    // Assert: Check that the status code is 409 Conflict
        //    Assert.Equal(StatusCodes.Status409Conflict, context.Response.StatusCode);

        //    // Optionally, assert that the response message is correct
        //    context.Response.Body.Seek(0, SeekOrigin.Begin);
        //    using (var reader = new StreamReader(context.Response.Body))
        //    {
        //        var responseMessage = await reader.ReadToEndAsync();
        //        Assert.Equal("Equipment already exists", responseMessage);
        //    }

        //    // Assert
        //    var conflictResult = Assert.IsType<ConflictObjectResult>(null);  // Expect 409 Conflict
        //    var conflictResponse = conflictResult.Value as dynamic;
        //    Assert.Equal("Equipment already exists", conflictResponse?.message);  // Verify the message
        //}
    }
}
