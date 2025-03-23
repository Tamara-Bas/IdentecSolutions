using IdentecSolutions.Domain.Enums;
using Xunit;
using EquipmentEntity = IdentecSolutions.Domain.Entities.Equipment;

namespace IdentecSolutions.Test.Domain.Entities
{
    public class EquipmentTest
    {
        [Fact]
        public void Equimpment_GetterSetter_Validation()
        {
            //Arrange
            int id = 1;
            string name = "test eq";
            string description = "test desc";
            string location = "base";
            decimal price = 100;
            string serialNumber = "123";
            EquipmentTypeEnum equipmentType = EquipmentTypeEnum.Internal;
            bool status = true;
            DateTime createdAt = new DateTime(2025, 01, 01);
            DateTime?  warrantyExpiryDate = new DateTime(2027, 01, 01);

            //Act
            EquipmentEntity actualEquipment = new EquipmentEntity()
            {
                Id = id,
                Name = name,
                Description = description,
                Location = location,
                Price = price,
                SerialNumber = serialNumber,
                EquipmentType = equipmentType,
                Status = status,
                CreatedAt = createdAt,
                WarrantyExpiryDate = warrantyExpiryDate

            };

            //Assert
            Assert.NotNull(actualEquipment);
            Assert.Equal(id, actualEquipment.Id);
            Assert.Equal(name, actualEquipment.Name);
            Assert.Equal(description, actualEquipment.Description);
            Assert.Equal(location, actualEquipment.Location);
            Assert.Equal(price, actualEquipment.Price);
            Assert.Equal(serialNumber, actualEquipment.SerialNumber);
            Assert.Equal(equipmentType, actualEquipment.EquipmentType);
            Assert.Equal(status, actualEquipment.Status);
            Assert.Equal(createdAt, actualEquipment.CreatedAt);
            Assert.Equal(warrantyExpiryDate, actualEquipment.WarrantyExpiryDate);

        }
    }
}
