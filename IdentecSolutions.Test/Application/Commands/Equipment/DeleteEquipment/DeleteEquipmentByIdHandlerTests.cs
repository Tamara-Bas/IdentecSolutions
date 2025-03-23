using IdentecSolutions.Application.Commands.Equipment.DeleteEquipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.Domain.Exceptions;
using IdentecSolutions.EF.UnitOfWork;
using Moq;
using Xunit;
using EquipmentEntity = IdentecSolutions.Domain.Entities.Equipment;

namespace IdentecSolutions.Test.Application.Commands.Equipment.DeleteEquipment
{
    public class DeleteEquipmentByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEquipmentServiceRepository> _equipmentServiceRepositoryMock;
        private readonly DeleteEquipmentByIdHandler _handler;

        public DeleteEquipmentByIdHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _equipmentServiceRepositoryMock = new Mock<IEquipmentServiceRepository>();
            _handler = new DeleteEquipmentByIdHandler(_unitOfWorkMock.Object, _equipmentServiceRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_SchoulDeleteEquipment()
        {
            // Arrange
            var request = new DeleteEquipmentByIdRequest
            {
                Id=1
            };

            EquipmentEntity dbEquipment = new()
            {
                Id = 1,
                Name = "name test",
                Description = "description test",
                Location = "location",
                Price = 100,
                SerialNumber = "123",
                EquipmentType = EquipmentTypeEnum.Internal,
                Status = true,
                WarrantyExpiryDate = new DateTime(2027, 01, 01)
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dbEquipment);
          
            _equipmentServiceRepositoryMock
                .Setup(s => s.DeleteEquipment(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.CreateTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenEquipmentDoesntExists()
        {
            // Arrange
            var request = new DeleteEquipmentByIdRequest
            {
                Id = 1
            };

            _equipmentServiceRepositoryMock
               .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
               .ReturnsAsync((EquipmentEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

            _equipmentServiceRepositoryMock.Verify(repo => repo.DeleteEquipment(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_RollsBackAndThrowsException()
        {
            // Arrange
            var request = new DeleteEquipmentByIdRequest
            {
                Id = 1
            };

            EquipmentEntity dbEquipment = new()
            {
                Id = 1,
                Name = "name test",
                Description = "description test",
                Location = "location",
                Price = 100,
                SerialNumber = "123",
                EquipmentType = EquipmentTypeEnum.Internal,
                Status = true,
                WarrantyExpiryDate = new DateTime(2027, 01, 01)
            };
            _equipmentServiceRepositoryMock
                 .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(dbEquipment);

            _equipmentServiceRepositoryMock
                .Setup(s => s.DeleteEquipment(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Failed to delete equipment", exception.Message);

            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
    }
}
