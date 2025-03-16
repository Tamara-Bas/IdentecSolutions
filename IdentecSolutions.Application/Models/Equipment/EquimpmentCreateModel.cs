namespace IdentecSolutions.Application.Models.Equipment
{
    public class EquimpmentCreateModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string SerialNumber { get; set; }
            public decimal Price { get; set; }
            public DateTime WarrantyExpiryDate { get; set; }
            public string Location { get; set; }
            public short EquipmentType { get; set; }
            public bool Status { get; set; }
        }
    
}
