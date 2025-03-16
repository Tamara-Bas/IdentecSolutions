using AutoMapper;
using IdentecSolutions.Application.Models.Equipment;

namespace IdentecSolutions.Application.Commands.Equipment.CreateEquipment
{
    public class CreateEquipmentMapper : Profile
    {
        public CreateEquipmentMapper()
        {
            //CreateMap<Models.Equipment.EquipmentDto, Domain.Entities.Equipment>().ReverseMap();
          
            CreateMap<EquimpmentCreateModel,  Domain.Entities.Equipment> ()
                .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name))
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Description))
                .ForMember(d => d.SerialNumber, x => x.MapFrom(s => s.SerialNumber))
                .ForMember(d => d.Price, x => x.MapFrom(s => s.Price))
                .ForMember(d => d.WarrantyExpiryDate, x => x.MapFrom(s => s.WarrantyExpiryDate))
                .ForMember(d => d.Location, x => x.MapFrom(s => s.Location))
                .ForMember(d => d.EquipmentType, x => x.MapFrom(s => (Domain.Enums.EquipmentTypeEnum)s.EquipmentType))
                .ForMember(d => d.Status, x => x.MapFrom(s => s.Status));
        }
    
    }
}
