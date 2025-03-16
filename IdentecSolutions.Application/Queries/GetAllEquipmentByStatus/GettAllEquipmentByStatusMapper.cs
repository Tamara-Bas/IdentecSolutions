using AutoMapper;
using IdentecSolutions.Application.Core.Common.Enum;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Domain.Entities;

namespace IdentecSolutions.Application.Queries.GetAllEquipmentByStatus
{
    class GettAllEquipmentByStatusMapper :Profile
    {
        public GettAllEquipmentByStatusMapper()
        {
            CreateMap<Equipment, EquipmentDto>()
               .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name))
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Description))
                .ForMember(d => d.SerialNumber, x => x.MapFrom(s => s.SerialNumber))
                .ForMember(d => d.WarrantyExpiryDate, x => x.MapFrom(s => s.WarrantyExpiryDate))
                .ForMember(d => d.Location, x => x.MapFrom(s => s.Location))
                .ForMember(d => d.EquipmentType, x => x.MapFrom(s => s.EquipmentType.GetDescription()))
                .ForMember(d => d.Price, x => x.MapFrom(s => s.Price))
                .ForMember(d => d.Status, x => x.MapFrom(s => s.Status));
        }
    }
}
