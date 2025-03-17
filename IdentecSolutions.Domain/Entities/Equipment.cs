using IdentecSolutions.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IdentecSolutions.Domain.Entities
{
    public class Equipment : Entity, IAuditable<AuditRecord>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 
        public string Description { get; set; }  
        public string SerialNumber { get; set; }  
        public decimal Price { get; set; }

        public DateTime? WarrantyExpiryDate { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; } //to do and think: should be enum???
        public EquipmentTypeEnum EquipmentType { get; set; }
        public AuditRecord AuditRecord { get ; set; } = new AuditRecord();
    }
}
