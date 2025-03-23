using IdentecSolutions.Application.Commands.Equipment.CreateEquipment;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.Domain.Exceptions;
using IdentecSolutions.EF.UnitOfWork;
using Moq;
using Xunit;
using EquipmentEntity = IdentecSolutions.Domain.Entities.Equipment;

namespace IdentecSolutions.Test.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEquipmentServiceRepository> _equipmentServiceRepositoryMock;
        private readonly CreateEquipmentHandler _handler;

        public CreateEquipmentHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _equipmentServiceRepositoryMock = new Mock<IEquipmentServiceRepository>();
            _handler = new CreateEquipmentHandler(_unitOfWorkMock.Object, _equipmentServiceRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_SchoulCreateEquipment()
        {
            // Arrange
            var request = new CreateEquipmentRequest
            {
                SerialNumber = "123",
                Name = "Test Equipment",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentBySerialNumber(request.SerialNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync((EquipmentEntity)null); // No existing equipment

            EquipmentEntity createEquipment = new()
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
                .Setup(s => s.CreateEquipment(It.IsAny<EquimpmentCreateModel>()))
                .ReturnsAsync(createEquipment);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.CreateTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }

        [Fact]
        public async Task Handle_ThrowsConflictException_WhenEquipmentAlreadyExists()
        {
            // Arrange
            var request = new CreateEquipmentRequest { SerialNumber = "123" };

            _equipmentServiceRepositoryMock
               .Setup(s => s.GetEquipmentBySerialNumber(request.SerialNumber, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new EquipmentEntity());

            // Act & Assert
            await Assert.ThrowsAsync<ConflictException>(() => _handler.Handle(request, CancellationToken.None));

            _equipmentServiceRepositoryMock.Verify(repo => repo.CreateEquipment(It.IsAny<EquimpmentCreateModel>()), Times.Never);
        }

        [Fact]
        public async Task Handle_RollsBackAndThrowsException()
        {
            // Arrange
            var request = new CreateEquipmentRequest
            {
                SerialNumber = "54321",
                Name = "Test Equipment",
                Description = "Test Description",
                Price = 100
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentBySerialNumber(request.SerialNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync((EquipmentEntity)null); // No existing equipment

            _equipmentServiceRepositoryMock
                .Setup(s => s.CreateEquipment(It.IsAny<EquimpmentCreateModel>()))
                .ReturnsAsync((EquipmentEntity)null); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Error occured", exception.Message);

            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }
    }
}
