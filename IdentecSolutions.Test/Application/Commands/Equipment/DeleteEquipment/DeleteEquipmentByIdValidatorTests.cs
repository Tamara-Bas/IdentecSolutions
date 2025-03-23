using FluentAssertions;
using IdentecSolutions.Application.Commands.Equipment.DeleteEquipment;
using Xunit;

namespace IdentecSolutions.Test.Application.Commands.Equipment.DeleteEquipment
{
    public class DeleteEquipmentByIdValidatorTests
    {
        private readonly DeleteEquipmentByIdValidator _validator;

        public DeleteEquipmentByIdValidatorTests()
        {
            _validator = new DeleteEquipmentByIdValidator();
        }

        [Fact]
        public async Task Validation_SchouldSucced_For_Valid_Request()
        {
            //Arrange
            var request = new DeleteEquipmentByIdRequest()
            {
                Id=1
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
            var request = new DeleteEquipmentByIdRequest();

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
            var request = new DeleteEquipmentByIdRequest()
            {
                Id=0
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Id must be grater than zero");
        }

    }
}
