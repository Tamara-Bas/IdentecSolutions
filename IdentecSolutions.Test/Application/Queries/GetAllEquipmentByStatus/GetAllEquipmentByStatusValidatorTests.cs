using FluentAssertions;
using IdentecSolutions.Application.Commands.Equipment.UpdateEquipment;
using IdentecSolutions.Application.Queries.GetAllEquipmentByStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IdentecSolutions.Test.Application.Queries.GetAllEquipmentByStatus
{
    public class GetAllEquipmentByStatusValidatorTests
    {
        private readonly GetAllEquipmentByStatusValidator _validator;

        public GetAllEquipmentByStatusValidatorTests()
        {
            _validator = new GetAllEquipmentByStatusValidator();
        }

        [Fact]
        public async Task Validation_SchouldSucced_For_Valid_Request()
        {
            //Arrange
            var request = new GetAllEquipmentByStatusRequest()
            {
                 Status=true
            };

            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Validation_SchouldFail_For_Empty_Status()
        {
            //Arrange
            var request = new GetAllEquipmentByStatusRequest();
            //Act
            var result = await _validator.ValidateAsync(request);

            //Assert
            result.IsValid.Should().BeFalse();
        }

    }
}
