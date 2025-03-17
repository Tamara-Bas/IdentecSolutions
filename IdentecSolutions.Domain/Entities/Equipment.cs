﻿using IdentecSolutions.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IdentecSolutions.Domain.Entities
{
    public class Equipment : Entity, IAudit
    {
        public string Name { get; set; } 
        public string Description { get; set; }  
        public string SerialNumber { get; set; }  
        public decimal Price { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public string Location { get; set; }
        public bool Status { get; set; } //to do and think: should be enum???
        public EquipmentTypeEnum EquipmentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get ; set ; }
    }
}
