using FluentAssertions;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using Xunit;

namespace IdentecSolutions.Test.Application.Queries.GetEquipmentById
{
    public class GetEquipmentByIdValidatorTests
    {

        private readonly GetEquipmentByIdValidator _validator;

        public GetEquipmentByIdValidatorTests()
        {
            _validator = new GetEquipmentByIdValidator();
        }

        [Fact]
        public async Task Validation_SchouldSucced_For_Valid_Request()
        {
            //Arrange
            var request = new GetEquipmentByIdRequest()
            {
                Id = 1
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Zero_Id()
        {
            //Arrange
            var request = new GetEquipmentByIdRequest()
            {
                Id = 0
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "Id parametar is required");
        }

    }
}
