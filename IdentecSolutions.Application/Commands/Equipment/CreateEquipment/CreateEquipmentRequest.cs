using IdentecSolutions.Application.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentecSolutions.Application.Commands.Equipment.CreateEquipment
{
    public sealed record CreateEquipmentRequest : BaseCommand
    {
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
