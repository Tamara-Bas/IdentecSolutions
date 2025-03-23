using AutoMapper;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Queries.GetAllEquipmentByStatus;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Entities;
using Moq;
using Xunit;

namespace IdentecSolutions.Test.Application.Queries.GetAllEquipmentByStatus
{
    public class GetAllEquipmentByStatusHandlerTests
    {
        private readonly Mock<IEquipmentServiceRepository> _equipmentServiceRepositoryMock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllEquipmentByStatusHandler _handler;

        public GetAllEquipmentByStatusHandlerTests()
        {
            _equipmentServiceRepositoryMock = new Mock<IEquipmentServiceRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllEquipmentByStatusHandler(_equipmentServiceRepositoryMock.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task Handle_ReturnsEquipmentList_WhenEquipmentListExist()
        {
            // Arrange
            var request = new GetAllEquipmentByStatusRequest
            {
                Status = true
            };

            var equipmentList = new List<Equipment>
            {
                new Equipment { Id = 1, Name = "name 1", Status = true },
                new Equipment { Id = 2, Name = "name 2", Status = true }
            };

            var mappedEquipmentList = new List<EquipmentDto>
            {
                new EquipmentDto { Id = equipmentList[0].Id, Name = "name 1", Status = true },
                new EquipmentDto { Id = equipmentList[1].Id, Name = "name 2", Status = true }
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetAllEquipmentByStatus(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(equipmentList);

            _mockMapper
                .Setup(mapper => mapper.Map<List<EquipmentDto>>(equipmentList))
                .Returns(mappedEquipmentList);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Total);
            Assert.Equal(mappedEquipmentList, response.Data);

            _equipmentServiceRepositoryMock.Verify(repo => repo.GetAllEquipmentByStatus(true, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<EquipmentDto>>(equipmentList), Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            var request = new GetAllEquipmentByStatusRequest
            {
                Status = true
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetAllEquipmentByStatus(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);

            _equipmentServiceRepositoryMock.Verify(s => s.GetAllEquipmentByStatus(true, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<List<EquipmentDto>>(It.IsAny<List<Equipment>>()), Times.Never);
        }
    }
}
