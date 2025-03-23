using FluentAssertions;
using IdentecSolutions.Application.Commands.Equipment.UpdateEquipment;
using Xunit;

namespace IdentecSolutions.Test.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentValidatorTests
    {
        private readonly UpdateEquipmentValidator _validator;

        public UpdateEquipmentValidatorTests()
        {
            _validator = new UpdateEquipmentValidator();
        }

        [Fact]
        public async Task Validation_SchouldSucced_For_Valid_Request()
        {
            //Arrange
            var request = new UpdateEquipmentRequest()
            {
                Id = 1
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_Id()
        {
            //Arrange
            var request = new UpdateEquipmentRequest();

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Id is required");
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Zero_Id()
        {
            //Arrange
            var request = new UpdateEquipmentRequest()
            {
                Id = 0
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Id must be grater than zero");
        }

    }
}
