using FluentAssertions;
using IdentecSolutions.Application.Commands.Equipment.CreateEquipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Enums;
using IdentecSolutions.EF.UnitOfWork;
using Moq;

using Xunit;

namespace IdentecSolutions.Test.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentValidatorTests
    {
        private readonly CreateEquipmentValidator _validator;

        public CreateEquipmentValidatorTests()
        {
            _validator = new CreateEquipmentValidator();
        }

        [Fact]
        public async Task Validation_SchouldSucced_For_Valid_Request()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
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

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_Name()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Name is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Null_Name()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = null,
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Name is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_When_NameLenghtIsGreaterThen50()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "Validation_SchouldFail_When_NameLenghtIsGreaterThen50Validation_SchouldFail_When_NameLenghtIsGreaterThen50Validation_SchouldFail_When_NameLenghtIsGreaterThen50",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Maximum length is 50");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_Description()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "name",
                Description = "",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Description is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Null_Description()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "name",
                Description = null,
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Description is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_SerialNumber()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "",
                Name = "name",
                Description = "desc",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Serial Number is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Null_SerialNumber()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = null,
                Name = "name",
                Description = "desv",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Serial Number is required");
        }


        [Fact]
        public async Task Validation_SchouldFail_For_ZeroValue_Price()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "1223",
                Name = "name",
                Description = "desv",
                Price = 0,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Warehouse",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Price must be greater than zero.");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_Location()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "name",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Location is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Null_Location()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name = "name",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = null,
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Location is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_When_LocationLenghtIsGreaterThen50()
        {
            //Arrange
            var request = new CreateEquipmentRequest()
            {
                SerialNumber = "123",
                Name="name",
                Description = "Test Description",
                Price = 100,
                WarrantyExpiryDate = DateTime.UtcNow.AddYears(1),
                Location = "Validation_SchouldFail_When_NameLenghtIsGreaterThen50Validation_SchouldFail_When_NameLenghtIsGreaterThen50Validation_SchouldFail_When_NameLenghtIsGreaterThen50",
                EquipmentType = (short)EquipmentTypeEnum.Internal,
                Status = true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Maximum length is 50");
        }

    }
}
