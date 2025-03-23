using AutoMapper;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Entities;
using IdentecSolutions.Domain.Exceptions;
using Moq;
using Xunit;
using EquipmentEntity = IdentecSolutions.Domain.Entities.Equipment;

namespace IdentecSolutions.Test.Application.Queries.GetEquipmentById
{
    public class GetEquipmentByIdHandlerTests
    {
        private readonly Mock<IEquipmentServiceRepository> _equipmentServiceRepositoryMock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetEquipmentByIdHandler _handler;

        public GetEquipmentByIdHandlerTests()
        {
            _equipmentServiceRepositoryMock = new Mock<IEquipmentServiceRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetEquipmentByIdHandler(_equipmentServiceRepositoryMock.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task Handle_ReturnsEquipment_WhenEquipmentIdExist()
        {
            // Arrange
            var request = new GetEquipmentByIdRequest
            {
                Id = 1
            };

            var dbEquipment = new Equipment
            {
                Id = 1, 
                Name = "name 1", 
                Status = true 
            };

            var mappedEquipment = new EquipmentDto
            {
                Id = 1,
                Name = "name 1", 
                Status = true 
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dbEquipment);

            _mockMapper
                .Setup(mapper => mapper.Map<EquipmentDto>(dbEquipment))
                .Returns(mappedEquipment);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(mappedEquipment, response.Data);

            _equipmentServiceRepositoryMock.Verify(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<EquipmentDto>(dbEquipment), Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            var request = new GetEquipmentByIdRequest
            {
                Id = 1
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);

            _equipmentServiceRepositoryMock.Verify(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<EquipmentDto>(It.IsAny<List<Equipment>>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ReturnsNotFoundException_WhenEquipmentNotFound()
        {
            // Arrange
            var request = new GetEquipmentByIdRequest
            {
                Id = 1
            };

            var dbEquipment = new EquipmentEntity
            {
                Id = 1,
                Name = "name 1",
                Status = true
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((EquipmentEntity)null);
            // Act
          //  var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));


            _equipmentServiceRepositoryMock.Verify(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<EquipmentDto>(dbEquipment), Times.Never);
        }
    }
}
