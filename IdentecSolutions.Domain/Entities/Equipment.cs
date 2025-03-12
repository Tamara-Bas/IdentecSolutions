using IdentecSolutions.Domain.Enums;

namespace IdentecSolutions.Domain.Entities
{
    public class Equipment : IEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; }  
        public string SerialNumber { get; set; }  
        public decimal Price { get; set; }
        public bool IsInUsage { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; } //to do and think: should be enum???
        public EquipmentTypeEnum EquipmentType { get; set; }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
