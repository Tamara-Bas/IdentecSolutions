using AutoMapper;
using IdentecSolutions.Application.Commands.Equipment.UpdateEquipment;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.Domain.Exceptions;
using IdentecSolutions.EF.UnitOfWork;
using Moq;
using Xunit;
using EquipmentEntity = IdentecSolutions.Domain.Entities.Equipment;

namespace IdentecSolutions.Test.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEquipmentServiceRepository> _equipmentServiceRepositoryMock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateEquipmentHandler _handler;

        public UpdateEquipmentHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _equipmentServiceRepositoryMock = new Mock<IEquipmentServiceRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateEquipmentHandler(_unitOfWorkMock.Object, _equipmentServiceRepositoryMock.Object,_mockMapper.Object);
        }

        [Fact]
        public async Task Handle_SchoulUpdateEquipment()
        {
            // Arrange
            var request = new UpdateEquipmentRequest
            {
               Id=1,
               Properties=new UpdateEquipmentBodyRequest
               {
                   Status=false
               }
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

            var updatedEquipmentModel = new UpdateEquipmentModel
            {
                Id = dbEquipment.Id,
                Status = request.Properties.Status,
                Name = dbEquipment.Name,
                Description = dbEquipment.Description,
                SerialNumber = dbEquipment.SerialNumber,
                Price = dbEquipment.Price,
                WarrantyExpiryDate = dbEquipment.WarrantyExpiryDate,
                Location = dbEquipment.Location,
                EquipmentType = (short)dbEquipment.EquipmentType
            };

            var updatedEquipment = new EquipmentEntity { Id = dbEquipment.Id, Status =false };

            var updatedDto = new EquipmentDto { Id = dbEquipment.Id, Status = false };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dbEquipment); 

            _equipmentServiceRepositoryMock
                .Setup(s => s.UpdateEquipment(It.IsAny<UpdateEquipmentModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(updatedEquipment);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(uow => uow.CreateTransaction(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Rollback(), Times.Never);
        }
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenEquipmentNotFound()
        {
            // Arrange
            var request = new UpdateEquipmentRequest
            {
                Id = 1,
                Properties = new UpdateEquipmentBodyRequest
                {
                    Status = false
                }
            };

            _equipmentServiceRepositoryMock
                .Setup(s => s.GetEquipmentById(request.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((EquipmentEntity)null); // Equipment not found

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Equipment not found", exception.Message);

            _equipmentServiceRepositoryMock.Verify(repo => repo.UpdateEquipment(It.IsAny<UpdateEquipmentModel>(), It.IsAny<CancellationToken>()), Times.Never);
        }
        [Fact]
        public async Task Handle_ThrowsException_WhenUpdateFails()
        {
            // Arrange
            var request = new UpdateEquipmentRequest
            {
                Id = 1,
                Properties = new UpdateEquipmentBodyRequest
                {
                    Status = false
                }
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
                .Setup(s => s.UpdateEquipment(It.IsAny<UpdateEquipmentModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((EquipmentEntity)null); // Simulate update failure

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Failed to update equipment", exception.Message);

            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Never);
        }

    }
}
