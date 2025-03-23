using IdentecSolutions.Application.Commands.Equipment.CreateEquipment;
using IdentecSolutions.Application.Commands.Equipment.UpdateEquipment;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Queries.GetAllEquipmentByStatus;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

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
        public async Task GettAllEquipmentByStatus_SchouldGetAllEquipment()
        {
            var query = new GetAllEquipmentByStatusRequest()
            {
                Status=true
            };

            var cancellationToken = CancellationToken.None;
            var equipmentList = new List<EquipmentDto>
            {
                new EquipmentDto { Id = 1, Name = "name 1", Status = true },
                new EquipmentDto { Id = 2, Name = "name 2", Status = true }
            };
            var expectedResult = new GetAllEquipmentByStatusResponse(equipmentList, 2);
      

            _queryDispatcherMock.Setup(x => x.QueryAsync(query, cancellationToken)).ReturnsAsync(expectedResult);

            var result = await _controller.GetAllEquipmentByStatus(query, cancellationToken);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }
        [Fact]
        public async Task GettEquipmentById_SchouldReturnEquipmentById()
        {
            var query = new GetEquipmentByIdRequest()
            {
                Id = 1
            };

            var cancellationToken = CancellationToken.None;
            var equipment = new EquipmentDto
            {
                Id = 1,
                Name = "name 1",
                Status = true
            };
            var expectedResult = new GetEquipmentByIdResponse(equipment);


            _queryDispatcherMock.Setup(x => x.QueryAsync(query, cancellationToken)).ReturnsAsync(expectedResult);

            var result = await _controller.GetEquipmentById(query, cancellationToken);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [Fact]
        public async Task CreateEquipment_SchouldCreateEquipment()
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
